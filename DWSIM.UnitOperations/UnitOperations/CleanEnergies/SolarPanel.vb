Imports System.IO
Imports DWSIM.Interfaces.Enums
Imports DWSIM.Interfaces.Enums.GraphicObjects
Imports DWSIM.UnitOperations.UnitOperations
Imports Eto.Forms
Imports DWSIM.UI.Shared.Common
Imports System.Globalization
Imports DWSIM.SharedClasses

Namespace UnitOperations

    Public Class SolarPanel

        Inherits CleanEnergyUnitOpBase

        Private ImagePath As String = ""

        Private Image As Object

        <Xml.Serialization.XmlIgnore> Public f As Object

        Public Overrides ReadOnly Property EquipmentTypes As List(Of String)
            Get
                Return New List(Of String) From {"", "Monocrystalline", "Polycrystalline", "Thin Film"}
            End Get
        End Property

        Public Overrides Sub CreateDimensionsList()

            Dimensions = New List(Of IDimension)
            Dimensions.Add(New Dimension With {.Name = DimensionName.Area, .IsUserDefined = False})
            Dimensions.Add(New Dimension With {.Name = DimensionName.Efficiency, .IsUserDefined = False})

        End Sub

        Public Overrides Sub UpdateDimensionsList()

            Dimensions(0).Value = PanelArea
            Dimensions(1).Value = PanelEfficiency

        End Sub

        Public Overrides Property Prefix As String = "SP-"

        Public Property PanelArea As Double = 1

        Public Property PanelEfficiency As Double = 15

        Public Property NumberOfPanels As Integer = 1

        Public Property GeneratedPower As Double = 0.0

        Public Property SolarIrradiation_kW_m2 As Double = 1.0

        Public Property ActualSolarIrradiation_kW_m2 As Double = 1.0

        Public Overrides Function GetDisplayName() As String
            Return "Solar Panel"
        End Function

        Public Overrides Function GetDisplayDescription() As String
            Return "Solar Panel"
        End Function

        Public Sub New()

            MyBase.New()

        End Sub

        Public Overrides Sub Draw(g As Object)
        End Sub

        Public Overrides Sub CreateConnectors()

            Dim w, h, x, y As Double
            w = GraphicObject.Width
            h = GraphicObject.Height
            x = GraphicObject.X
            y = GraphicObject.Y

            Dim myOC1 As New Object
            myOC1.Position = New Object()
            myOC1.Type = ConType.ConOut
            myOC1.Direction = ConDir.Right
            myOC1.Type = ConType.ConEn

            With GraphicObject.OutputConnectors
                If .Count = 1 Then
                    .Item(0).Position = New Object()
                Else
                    .Add(myOC1)
                End If
                .Item(0).ConnectorName = "Power Outlet"
            End With

            Me.GraphicObject.EnergyConnector.Active = False

        End Sub


        Public Overrides Sub PopulateEditorPanel(ctner As Object)
        End Sub

        Public Overrides Function GetReport(su As IUnitsOfMeasure, ci As CultureInfo, nf As String) As String

            Dim sb As New Text.StringBuilder()

            sb.AppendLine(String.Format("Number of Units: {0}", NumberOfPanels))

            sb.AppendLine()
            sb.AppendLine(String.Format("Using Global Weather: {0}", Not UseUserDefinedWeather))
            sb.AppendLine(String.Format("Solar Irradiation: {0} kW/m2", SolarIrradiation_kW_m2.ToString(nf)))

            sb.AppendLine()
            sb.AppendLine(String.Format("Panel Area: {0} {1}", PanelArea.ConvertFromSI(su.area).ToString(nf), su.area))
            sb.AppendLine(String.Format("Efficiency: {0}", PanelEfficiency.ToString(nf)))
            sb.AppendLine()
            sb.AppendLine(String.Format("Generated Power: {0} {1}", GeneratedPower.ConvertFromSI(su.heatflow).ToString(nf), su.heatflow))

            Return sb.ToString()

        End Function

        Public Overrides Function ReturnInstance(typename As String) As Object

            Return New SolarPanel

        End Function



        Public Overrides Function CloneXML() As Object

            Dim obj As ICustomXMLSerialization = New SolarPanel()
            obj.LoadData(Me.SaveData)
            Return obj

        End Function

        Public Overrides Function CloneJSON() As Object

            Throw New NotImplementedException()

        End Function

        Public Overrides Function LoadData(data As System.Collections.Generic.List(Of System.Xml.Linq.XElement)) As Boolean

            Dim ci As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture

            XMLSerializer.XMLSerializer.Deserialize(Me, data)

            Return True

        End Function

        Public Overrides Function SaveData() As System.Collections.Generic.List(Of System.Xml.Linq.XElement)

            Dim elements As System.Collections.Generic.List(Of System.Xml.Linq.XElement) = XMLSerializer.XMLSerializer.Serialize(Me)
            Dim ci As Globalization.CultureInfo = Globalization.CultureInfo.InvariantCulture

            Return elements

        End Function

        Public Overrides Sub Calculate(Optional args As Object = Nothing)

            Dim esout = GetOutletEnergyStream(0)

            Dim si As Double = 0.0

            If UseUserDefinedWeather Then

                si = SolarIrradiation_kW_m2

            Else

                si = FlowSheet.FlowsheetOptions.CurrentWeather.SolarIrradiation_kWh_m2

            End If

            ActualSolarIrradiation_kW_m2 = si

            GeneratedPower = si * PanelArea * NumberOfPanels * PanelEfficiency / 100.0

            esout.EnergyFlow = GeneratedPower

        End Sub


        Public Overrides Function GetProperties(proptype As PropertyType) As String()

            Select Case proptype
                Case PropertyType.ALL, PropertyType.RW, PropertyType.RO
                    Return New String() {"Efficiency", "User-Defined Solar Irradiation", "Actual Solar Irradiation", "Panel Area", "Number of Panels", "Generated Power"}
                Case PropertyType.WR
                    Return New String() {"Efficiency", "User-Defined Solar Irradiation", "Panel Area", "Number of Panels"}
            End Select

            Return New String() {}

        End Function

        Public Overrides Function GetPropertyValue(prop As String, Optional su As IUnitsOfMeasure = Nothing) As Object

            If su Is Nothing Then su = New SharedClasses.SystemsOfUnits.SI

            Select Case prop
                Case "Efficiency"
                    Return PanelEfficiency
                Case "User-Defined Solar Irradiation"
                    Return SolarIrradiation_kW_m2
                Case "Actual Solar Irradiation"
                    Return ActualSolarIrradiation_kW_m2
                Case "Panel Area"
                    Return PanelArea.ConvertFromSI(su.area)
                Case "Number of Panels"
                    Return NumberOfPanels
                Case "Generated Power"
                    Return GeneratedPower.ConvertFromSI(su.heatflow)
            End Select

            Return Nothing

        End Function

        Public Overrides Function GetPropertyUnit(prop As String, Optional su As IUnitsOfMeasure = Nothing) As String

            If su Is Nothing Then su = New SharedClasses.SystemsOfUnits.SI

            Select Case prop
                Case "Efficiency"
                    Return "%"
                Case "User-Defined Solar Irradiation", "Actual Solar Irradiation"
                    Return "kW/m2"
                Case "Panel Area"
                    Return (su.area)
                Case "Number of Panels"
                    Return ""
                Case "Generated Power"
                    Return (su.heatflow)
            End Select

            Return String.Empty

        End Function

        Public Overrides Function SetPropertyValue(prop As String, propval As Object, Optional su As IUnitsOfMeasure = Nothing) As Boolean

            If su Is Nothing Then su = New SharedClasses.SystemsOfUnits.SI

            Select Case prop
                Case "Efficiency"
                    PanelEfficiency = propval
                Case "User-Defined Solar Irradiation"
                    SolarIrradiation_kW_m2 = propval
                Case "Panel Area"
                    PanelArea = Convert.ToDouble(propval).ConvertToSI(su.area)
                Case "Number of Panels"
                    NumberOfPanels = propval
            End Select

            Return True

        End Function

    End Class

End Namespace