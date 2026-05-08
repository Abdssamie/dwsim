Imports DWSIM.Interfaces
Imports DWSIM.Interfaces.Enums.GraphicObjects
Imports System.Runtime.InteropServices
Imports System.IO

Public Class ConnectionPointStub
    Implements IConnectionPoint

    Public Property X As Integer Implements IConnectionPoint.X
    Public Property Y As Integer Implements IConnectionPoint.Y
    Public Property Type As ConType Implements IConnectionPoint.Type
    Public Property Direction As ConDir Implements IConnectionPoint.Direction
    Public Property AttachedConnector As IConnectorGraphicObject Implements IConnectionPoint.AttachedConnector
    Public Property IsAttached As Boolean Implements IConnectionPoint.IsAttached
    Public Property ConnectorName As String Implements IConnectionPoint.ConnectorName
    Public Property Position As IPoint Implements IConnectionPoint.Position
    Public Property IsEnergyConnector As Boolean Implements IConnectionPoint.IsEnergyConnector
    Public Property Active As Boolean Implements IConnectionPoint.Active

    Public Sub New()
    End Sub
End Class

' Mock for ConnectionPoint if used as a type
Public Class ConnectionPoint
    Inherits ConnectionPointStub
End Class

Public Class GraphicObject
    Implements IGraphicObject

    Public Shared Function ReturnInstance(typename As String) As IGraphicObject
        Return New GraphicObject()
    End Function

    Public Property DoubleClickAction As Action(Of Object) Implements IGraphicObject.DoubleClickAction
    Public Property DrawOverride As Action(Of Object) Implements IGraphicObject.DrawOverride
    Public Property Editor As Object Implements IGraphicObject.Editor
    Public Property ShapeOverride As ShapeIcon Implements IGraphicObject.ShapeOverride
    Public Property Status As Status Implements IGraphicObject.Status
    Public Property Description As String Implements IGraphicObject.Description
    Public Property AdditionalInfo As Object Implements IGraphicObject.AdditionalInfo
    Public Property Shape As Integer Implements IGraphicObject.Shape
    Public Property FlippedH As Boolean Implements IGraphicObject.FlippedH
    Public Property FlippedV As Boolean Implements IGraphicObject.FlippedV
    Public Property ObjectType As ObjectType Implements IGraphicObject.ObjectType
    Public Property IsEnergyStream As Boolean Implements IGraphicObject.IsEnergyStream
    Public Property Active As Boolean Implements IGraphicObject.Active
    Public Property Tag As String Implements IGraphicObject.Tag
    Public Property AutoSize As Boolean Implements IGraphicObject.AutoSize
    Public Property IsConnector As Boolean Implements IGraphicObject.IsConnector
    Public Property X As Single Implements IGraphicObject.X
    Public Property Y As Single Implements IGraphicObject.Y
    Public Property Name As String Implements IGraphicObject.Name
    Public Property Height As Integer Implements IGraphicObject.Height
    Public Property Width As Integer Implements IGraphicObject.Width
    Public Property InputConnectors As List(Of IConnectionPoint) = New List(Of IConnectionPoint) Implements IGraphicObject.InputConnectors
    Public Property OutputConnectors As List(Of IConnectionPoint) = New List(Of IConnectionPoint) Implements IGraphicObject.OutputConnectors
    Public Property SpecialConnectors As List(Of IConnectionPoint) = New List(Of IConnectionPoint) Implements IGraphicObject.SpecialConnectors
    Public Property EnergyConnector As IConnectionPoint Implements IGraphicObject.EnergyConnector
    Public Property Rotation As Integer Implements IGraphicObject.Rotation
    Public Property Calculated As Boolean Implements IGraphicObject.Calculated
    Public Property Position As IPoint Implements IGraphicObject.Position
    Public Property Selected As Boolean Implements IGraphicObject.Selected
    Public Property Owner As ISimulationObject Implements IGraphicObject.Owner
    Public Property Extensions As Dictionary(Of String, IGraphicObjectExtension) = New Dictionary(Of String, IGraphicObjectExtension) Implements IGraphicObject.Extensions
    Public Property DrawMode As Integer Implements IGraphicObject.DrawMode
    Public Property FontStyle As FontStyle Implements IGraphicObject.FontStyle
    Public Property Flowsheet As IFlowsheet Implements IGraphicObject.Flowsheet
    Public Property DrawLabel As Boolean Implements IGraphicObject.DrawLabel

    ' Missing properties found in build errors
    Public Property LineWidth As Integer = 1
    Public Property Fill As Boolean = False

    Public Sub New()
    End Sub

    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        Me.X = x
        Me.Y = y
        Me.Width = w
        Me.Height = h
    End Sub

    Public Sub PositionConnectors() Implements IGraphicObject.PositionConnectors
    End Sub

    Public Sub Draw(surface As Object) Implements IGraphicObject.Draw
    End Sub

    Public Function Clone() As IGraphicObject Implements IGraphicObject.Clone
        Return DirectCast(Me.MemberwiseClone(), IGraphicObject)
    End Function

    Public Function HitTest(zoomedSelection As Object) As Boolean Implements IGraphicObject.HitTest
        Return False
    End Function

    Public Function GetPointValue(type As PointValueType, Xref As Integer, Yref As Integer, args As List(Of Object)) As Double Implements IGraphicObject.GetPointValue
        Return 0.0
    End Function

    Public Function GetIconAsStream() As MemoryStream Implements IGraphicObject.GetIconAsStream
        Return New MemoryStream()
    End Function

    Public Sub ReleaseReferences() Implements IGraphicObject.ReleaseReferences
    End Sub

    Public Sub CreateConnectors(i As Integer, j As Integer)
    End Sub

    Public Sub LoadData(data As List(Of XElement))
    End Sub

    Public Function SaveData() As List(Of XElement)
        Return New List(Of XElement)()
    End Function

    Public Sub SetSize(size As SKSize)
        Me.Width = CInt(size.Width)
        Me.Height = CInt(size.Height)
    End Sub

End Class

Public Class Extended
    Public Class [Shared]
        Public Shared Function ReturnInstance(typename As String) As IGraphicObject
            Return New GraphicObject()
        End Function
    End Class
End Class

' Stubs for specific graphic types
Public Class MaterialStreamGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class EnergyStreamGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class MixerGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class SplitterGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class PumpGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class TankGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class VesselGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class CompressorGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class TurbineGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class CoolerGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class HeaterGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class PipeSegmentGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class ValveGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class ConversionReactorGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class EquilibriumReactorGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class GibbsReactorGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class CSTRGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class PFRGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class HeatExchangerGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class ShortcutColumnGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class RigorousColumnGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class AbsorptionColumnGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class ComponentSeparatorGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class SolidsSeparatorGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class FilterGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class OrificePlateGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class ScriptGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class SpreadsheetGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class FlowsheetGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class CAPEOPENGraphic
    Inherits GraphicObject
    Public Property ChemSep As Boolean
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class ExternalUnitOperationGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class SwitchGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class InputGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class PIDControllerGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class PythonControllerGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class LevelGaugeGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class DigitalGaugeGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class AnalogGaugeGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class AdjustGraphic
    Inherits GraphicObject
    Public Property ConnectedToMv As IGraphicObject
    Public Property ConnectedToCv As IGraphicObject
    Public Property ConnectedToRv As IGraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class SpecGraphic
    Inherits GraphicObject
    Public Property ConnectedToTv As IGraphicObject
    Public Property ConnectedToSv As IGraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class RecycleGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class EnergyRecycleGraphic
    Inherits GraphicObject
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class TableGraphic
    Inherits GraphicObject
    Public Property Flowsheet As Object
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class MasterTableGraphic
    Inherits GraphicObject
    Public Property Flowsheet As Object
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class SpreadsheetTableGraphic
    Inherits GraphicObject
    Public Property Flowsheet As Object
    Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
        MyBase.New(x, y, w, h)
    End Sub
End Class

Public Class SKSize
    Public Property Width As Double
    Public Property Height As Double
    Public Sub New(w As Double, h As Double)
        Me.Width = w
        Me.Height = h
    End Sub
End Class

Namespace Charts
    Public Class OxyPlotGraphic
        Inherits GraphicObject
        Public Property Flowsheet As Object
        Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
            MyBase.New(x, y, w, h)
        End Sub
    End Class
End Namespace

Namespace Shapes
    Public Class CAPEOPENGraphic
        Inherits GraphicObject
        Public Property ChemSep As Boolean
        Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
            MyBase.New(x, y, w, h)
        End Sub
    End Class
    Public Class AdjustGraphic
        Inherits GraphicObject
        Public Property ConnectedToMv As IGraphicObject
        Public Property ConnectedToCv As IGraphicObject
        Public Property ConnectedToRv As IGraphicObject
        Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
            MyBase.New(x, y, w, h)
        End Sub
    End Class
    Public Class SpecGraphic
        Inherits GraphicObject
        Public Property ConnectedToTv As IGraphicObject
        Public Property ConnectedToSv As IGraphicObject
        Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
            MyBase.New(x, y, w, h)
        End Sub
    End Class
    Public Class RigorousColumnGraphic
        Inherits GraphicObject
        Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
            MyBase.New(x, y, w, h)
        End Sub
    End Class
    Public Class AbsorptionColumnGraphic
        Inherits GraphicObject
        Public Sub New(x As Single, y As Single, w As Integer, h As Integer)
            MyBase.New(x, y, w, h)
        End Sub
    End Class
End Namespace
