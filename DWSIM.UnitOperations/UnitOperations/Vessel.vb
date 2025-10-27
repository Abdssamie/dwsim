'    Separator Vessel Calculation Routines 
'    Copyright 2008-2025 Daniel Wagner O. de Medeiros
'
'    This file is part of DWSIM.
'
'    DWSIM is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    DWSIM is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with DWSIM.  If not, see <http://www.gnu.org/licenses/>.


Imports DWSIM.Thermodynamics
Imports DWSIM.Thermodynamics.Streams
Imports DWSIM.SharedClasses
Imports DWSIM.Interfaces.Enums
Imports DWSIM.UnitOperations.UnitOperations.Auxiliary.Pipe

Namespace UnitOperations

    <System.Serializable()> Public Class Vessel

        Inherits UnitOperations.UnitOpBaseClass

        Public Overrides Property ObjectClass As SimulationObjectClass = SimulationObjectClass.Separators

        Public Shared Property MaterialTypes As List(Of String) = New List(Of String)({"Steel", "Carbon Steel", "Cast Iron", "Stainless Steel", "Commercial Copper"})

        Public Shared Property HeadTypes As List(Of String) = New List(Of String)({"Ellipsoidal (2:1)", "Hemispherical", "Torispherical (ASME F&D)", "Torispherical (Standard F&D)", "Torispherical (80:10 F&D)", "Flat"})

        Public Property ThermalProperties As New ThermalEditorDefinitions

        Public Property WallThickness As Double = 0.01 'm

        Public Property WallMaterial As String = "Carbon Steel"

        Public Property WallTemperature As Double = 0.0

        Public Property HeadType As String = "Hemispherical"

        Public Property CalculateRigorousHeatBalance As Boolean = False

        Public Overrides ReadOnly Property SupportsDynamicMode As Boolean = True

        Public Overrides ReadOnly Property HasPropertiesForDynamicMode As Boolean = True

        Public Overrides ReadOnly Property EquipmentTypes As List(Of String)
            Get
                Return New List(Of String) From {"", "Vertical", "Horizontal"}
            End Get
        End Property

        Public Overrides Sub CreateDimensionsList()

            Dimensions = New List(Of IDimension)
            Dimensions.Add(New Dimension With {.Name = DimensionName.Diameter, .IsUserDefined = False})
            Dimensions.Add(New Dimension With {.Name = DimensionName.Length, .IsUserDefined = False})

        End Sub

        Public Overrides Sub UpdateDimensionsList()

            If SelectedEquipmentType = "Horizontal" Then
                Dimensions(0).Value = DH * 1000
                Dimensions(1).Value = AH
            Else
                Dimensions(0).Value = DV * 1000
                Dimensions(1).Value = AV
            End If

        End Sub

        Dim rhol, rhov, ql, qv, qe, rhoe, wl, wv As Double
        Dim C, VGI, VMAX, K As Double
        Dim BeH, BSGH, BSLH As Double
        Public AH, DH As Double
        Dim BeV, BSGV, BSLV As Double
        Public AV, DV As Double

        <NonSerialized> <Xml.Serialization.XmlIgnore> Public f As EditingForm_Vessel

        <NonSerialized> <Xml.Serialization.XmlIgnore> Public MixedStream As MaterialStream

        Protected m_DQ As Nullable(Of Double)

        Public Enum PressureBehavior
            Average = 0
            Maximum = 1
            Minimum = 2
        End Enum

        Public Enum CalculationModes
            Adiabatic = 0
            Legacy = 1
            HeatingCoolingIsothermic = 2
            HeatingCoolingIsobaric = 3
        End Enum

        Public Property CalculationMode As CalculationModes = CalculationModes.Legacy

        Public Property DimensionRatio As Double = 3

        Public Property SurgeFactor As Double = 1.2

        Public Property ResidenceTime As Double = 5

        Public Property PressureCalculation() As PressureBehavior = PressureBehavior.Minimum

        Public Enum OperationMode
            TwoPhase = 0
            ThreePhase = 1
        End Enum

        Public Property OverrideT As Boolean = False

        Public Property OverrideP As Boolean = False

        Public Property FlashPressure As Double = 101325

        Public Property FlashTemperature As Double = 298.15

        Public Property DeltaQ As Nullable(Of Double)

        Public Sub New()

            MyBase.New()

        End Sub

        Public Sub New(ByVal name As String, ByVal description As String)

            MyBase.CreateNew()
            Me.ComponentName = name
            Me.ComponentDescription = description

        End Sub

        Public Overrides Function CloneXML() As Object
            Dim obj As ICustomXMLSerialization = New Vessel()
            obj.LoadData(Me.SaveData)
            Return obj
        End Function

        Public Overrides Function CloneJSON() As Object
            Return Newtonsoft.Json.JsonConvert.DeserializeObject(Of Vessel)(Newtonsoft.Json.JsonConvert.SerializeObject(Me))
        End Function

        Public Overrides Sub CreateDynamicProperties()

            AddDynamicProperty("Vessel Orientation", "Vertical or Horizontal (V = 0, H = 1)", 0, UnitOfMeasure.none, 1.0.GetType())
            AddDynamicProperty("Operating Pressure", "Current Vessel Operating Pressure", 0, UnitOfMeasure.pressure, 1.0.GetType())
            AddDynamicProperty("Liquid Level", "Current Liquid Level", 0, UnitOfMeasure.distance, 1.0.GetType())
            AddDynamicProperty("Get Volume from Dimensions", "Calculate volume from dimensions (Diameter, Height and Head Type)", False, UnitOfMeasure.none, True.GetType())
            AddDynamicProperty("Volume", "Vessel Volume (define if no dimensions set)", 1, UnitOfMeasure.volume, 1.0.GetType())
            AddDynamicProperty("Get Height from Dimensions", "Use Height from Dimensions", False, UnitOfMeasure.none, True.GetType())
            AddDynamicProperty("Height", "Available height for liquid (define if no dimensions set)", 2, UnitOfMeasure.distance, 1.0.GetType())
            AddDynamicProperty("Minimum Pressure", "Minimum dynamic pressure", 101325, UnitOfMeasure.pressure, 1.0.GetType())
            AddDynamicProperty("Initialize using Inlet Stream", "Initializes the vessel content with information from the inlet stream, if the vessel content is null", True, UnitOfMeasure.none, True.GetType())
            AddDynamicProperty("Reset Content", "Empties the vessel's content on the next run", False, UnitOfMeasure.none, True.GetType())

        End Sub

        Public Overrides Sub DisplayDynamicsEditForm()

            If fd Is Nothing Then
                fd = New DynamicsPropertyEditor With {.SimObject = Me}
                fd.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
                fd.Tag = "ObjectEditor"
                fd.UpdateCallBack = Sub(table)
                                        AddButtonsToDynEditor(table)
                                    End Sub
                Me.FlowSheet.DisplayForm(fd)
            Else
                If fd.IsDisposed Then
                    fd = New DynamicsPropertyEditor With {.SimObject = Me}
                    fd.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
                    fd.Tag = "ObjectEditor"
                    fd.UpdateCallBack = Sub(table)
                                            AddButtonsToDynEditor(table)
                                        End Sub
                    Me.FlowSheet.DisplayForm(fd)
                Else
                    fd.Activate()
                End If
            End If

        End Sub

        Private Sub AddButtonsToDynEditor(table As TableLayoutPanel)

            Dim button1 As New Button With {.Text = FlowSheet.GetTranslatedString("ViewAccumulationStream"),
                .Dock = DockStyle.Bottom, .AutoSize = True, .AutoSizeMode = AutoSizeMode.GrowAndShrink}
            AddHandler button1.Click, Sub(s, e)
                                          AccumulationStream.SetFlowsheet(FlowSheet)
                                          Dim fms As New MaterialStreamEditor With {
                                          .MatStream = AccumulationStream,
                                          .IsAccumulationStream = True,
                                          .Text = Me.GraphicObject.Tag + ": " + FlowSheet.GetTranslatedString("AccumulationStream")}
                                          FlowSheet.DisplayForm(fms)
                                      End Sub

            Dim button2 As New Button With {.Text = FlowSheet.GetTranslatedString("FillWithStream"),
                .Dock = DockStyle.Bottom, .AutoSize = True, .AutoSizeMode = AutoSizeMode.GrowAndShrink}
            AddHandler button2.Click, Sub(s, e)
                                          AccumulationStream?.SetFlowsheet(FlowSheet)
                                          Dim fms As New EditingForm_SeparatorFiller With {.SimObject = Me}
                                          fms.ShowDialog()
                                      End Sub

            table.Controls.Add(button1)
            table.Controls.Add(button2)
            table.Controls.Add(New Panel())

        End Sub

        Public Function CalculateVolume() As Double

            Dim Vol As Double = GetDynamicProperty("Volume")

            Dim Height As Double = GetDynamicProperty("Height")

            If GetDynamicProperty("Get Height from Dimensions") Then

                Height = Dimensions(1).Value

            End If

            Dim D, L, DE As Double

            If GetDynamicProperty("Get Volume from Dimensions") Then

                ' Calculate vessel volume

                Dim pi = Math.PI

                D = Dimensions(0).Value / 1000
                L = Dimensions(1).Value
                DE = D + WallThickness

                Select Case HeadType

                    Case "Ellipsoidal (2:1)"

                        Vol = pi * D ^ 2 * L / 4 + 2 * (pi * D ^ 3 / 24)

                    Case "Hemispherical"

                        Vol = pi * D ^ 2 * L / 4 + 2 * (pi * D ^ 3 / 12)

                    Case "Torispherical (ASME F&D)"

                        Vol = pi * D ^ 2 * L / 4 + 2 * (0.0847 * D ^ 3)

                    Case "Torispherical (Standard F&D)"

                        Vol = pi * D ^ 2 * L / 4 + 2 * (0.0808 * D ^ 3)

                    Case "Torispherical (80:10 F&D)"

                        Vol = pi * D ^ 2 * L / 4 + 2 * (0.0746 * D ^ 3)

                    Case "Flat"

                        Vol = pi * D ^ 2 * L / 4

                End Select

            End If

            Return Vol

        End Function

        Private prevM, currentM As Double

        Public Overrides Sub RunDynamicModel()

            Dim integratorID = FlowSheet.DynamicsManager.ScheduleList(FlowSheet.DynamicsManager.CurrentSchedule).CurrentIntegrator
            Dim integrator = FlowSheet.DynamicsManager.IntegratorList(integratorID)

            Dim timestep = integrator.IntegrationStep.TotalSeconds

            If integrator.RealTime Then timestep = Convert.ToDouble(integrator.RealTimeStepMs) / 1000.0

            Dim oms1 As MaterialStream = Me.GetOutletMaterialStream(0)
            Dim oms2 As MaterialStream = Me.GetOutletMaterialStream(1)

            Dim oms3 As MaterialStream = Me.GetOutletMaterialStream(2)

            Dim omsr As MaterialStream = GetOutletMaterialStream(3)

            If CalculationMode > 1 Then
                Throw New Exception("Only Adiabatic and Legacy mode are supported in dynamic mode.")
            End If

            If oms3 IsNot Nothing Then
                Throw New Exception("The Gas-Liquid Separator currently supports only a single liquid phase in Dynamic Mode.")
            End If

            Dim imsmix As MaterialStream = Nothing

            For i = 0 To 5
                If Me.GraphicObject.InputConnectors(i).IsAttached Then
                    Dim imsx = GetInletMaterialStream(i)
                    If imsmix Is Nothing Then 
											imsmix = imsx.CloneXML()
										Else
	                    If Not Double.IsNaN(imsx.GetMassFlow()) AndAlso imsx.GetMassFlow() > 0 Then imsmix = imsmix.Add(imsx)
										End If
                End If
            Next

            Dim Vol = CalculateVolume()

            Dim Pressure, Enthalpy As Double
            Dim Pmin = GetDynamicProperty("Minimum Pressure")
            Dim Orientation As Integer = GetDynamicProperty("Vessel Orientation")
            Dim InitializeFromInlet As Boolean = GetDynamicProperty("Initialize using Inlet Stream")

            Dim Reset As Boolean = GetDynamicProperty("Reset Content")

            If Reset Then
                AccumulationStream = Nothing
                SetDynamicProperty("Reset Content", 0)
            End If

            If AccumulationStream Is Nothing Then

                If InitializeFromInlet Then

                    AccumulationStream = imsmix.CloneXML

                Else

                    AccumulationStream = imsmix.Subtract(oms1, timestep)
                    AccumulationStream = AccumulationStream.Subtract(oms2, timestep)

                End If

                Dim density = AccumulationStream.Phases(0).Properties.density.GetValueOrDefault

                AccumulationStream.SetMassFlow(density * Vol)
                AccumulationStream.SpecType = StreamSpec.Temperature_and_Pressure
                AccumulationStream.PropertyPackage = PropertyPackage
                AccumulationStream.PropertyPackage.CurrentMaterialStream = AccumulationStream
                AccumulationStream.Calculate()

            Else

                AccumulationStream.SetFlowsheet(FlowSheet)

                If Not imsmix.AtEquilibrium And imsmix.GetMassFlow() > 0 Then
                    imsmix.AssignSelfToPP()
                    imsmix.Calculate()
                End If

                If imsmix.GetMassFlow() > 0 Then
                    AccumulationStream = AccumulationStream.Add(imsmix, timestep)
                End If

                AccumulationStream.PropertyPackage.CurrentMaterialStream = AccumulationStream

                AccumulationStream.Calculate()

                If Not oms1.AtEquilibrium And oms1.GetMassFlow() > 0 Then
                    oms1.AssignSelfToPP()
                    oms1.Calculate()
                End If

                If Not oms2.AtEquilibrium And oms2.GetMassFlow() > 0 Then
                    oms2.AssignSelfToPP()
                    oms2.Calculate()
                End If

                If omsr IsNot Nothing AndAlso (Not omsr.AtEquilibrium And omsr.GetMassFlow() > 0) Then
                    omsr.AssignSelfToPP()
                    omsr.Calculate()
                End If

                If oms1.GetMassFlow() > 0 Then AccumulationStream = AccumulationStream.Subtract(oms1, timestep)
                If oms2.GetMassFlow() > 0 Then AccumulationStream = AccumulationStream.Subtract(oms2, timestep)
                If omsr IsNot Nothing Then
                    If omsr.GetMassFlow() > 0 Then AccumulationStream = AccumulationStream.Subtract(omsr, timestep)
                End If

                If AccumulationStream.GetMassFlow() <= 0.0 Then AccumulationStream.SetMassFlow(0.0)

            End If

            AccumulationStream.SetFlowsheet(FlowSheet)

            Dim D = Dimensions(0).Value
            Dim L = Dimensions(1).Value
            Dim DE = D + WallThickness

            Dim Height As Double = GetDynamicProperty("Height")

            If GetDynamicProperty("Get Height from Dimensions") Then Height = Dimensions(1).Value

            ' Calculate Temperature

            Dim Qval, Ha, Wa As Double

            Ha = AccumulationStream.GetMassEnthalpy
            Wa = AccumulationStream.GetMassFlow

            Dim es = GetInletEnergyStream(6)

            If CalculateRigorousHeatBalance Then

                If es IsNot Nothing Then

                    Throw New Exception("Please disconnect the energy stream to calculate the rigorous heat balance.")

                End If

                Dim Uint, Uext, A, DQ, DQmax, Twall, Tint, Tpe, Cp_m, holdup, Cpl, Cpv, Text, Kl, Kv, VapVel, LiqVel, MUl, MUv As Double

                Tint = AccumulationStream.GetTemperature()

                Twall = WallTemperature

                If ThermalProperties.TipoPerfil = ThermalEditorDefinitions.ThermalProfileType.Definir_CGTC Then
                    Text = ThermalProperties.Temp_amb_definir
                ElseIf ThermalProperties.TipoPerfil = ThermalEditorDefinitions.ThermalProfileType.Estimar_CGTC Then
                    Text = ThermalProperties.Temp_amb_estimar
                End If

                A = Math.PI * (Dimensions(0).Value + WallThickness) * Dimensions(1).Value

                Cpl = AccumulationStream.OverallLiquid.Properties.heatCapacityCp.GetValueOrDefault()
                Cpv = AccumulationStream.Vapor.Properties.heatCapacityCp.GetValueOrDefault()
                Kl = AccumulationStream.OverallLiquid.Properties.thermalConductivity.GetValueOrDefault()
                Kv = AccumulationStream.Vapor.Properties.thermalConductivity.GetValueOrDefault()
                MUl = AccumulationStream.OverallLiquid.Properties.viscosity.GetValueOrDefault()
                MUv = AccumulationStream.Vapor.Properties.heatCapacityCp.GetValueOrDefault()
                rhol = AccumulationStream.OverallLiquid.Properties.density.GetValueOrDefault()
                rhov = AccumulationStream.Vapor.Properties.density.GetValueOrDefault()

                holdup = AccumulationStream.GetMassFlow() * AccumulationStream.OverallLiquid.Properties.massfraction.GetValueOrDefault() / rhol / Vol

                VapVel = 0.0 'AccumulationStream.GetMassFlow() * AccumulationStream.Vapor.Properties.massfraction.GetValueOrDefault() / rhov / A
                LiqVel = 0.0 'AccumulationStream.GetMassFlow() * AccumulationStream.OverallLiquid.Properties.massfraction.GetValueOrDefault() / rhol / A

                Cp_m = holdup * Cpl + (1 - holdup) * Cpv

                If Not ThermalProperties.TipoPerfil = ThermalEditorDefinitions.ThermalProfileType.Definir_Q Then
                    If ThermalProperties.TipoPerfil = ThermalEditorDefinitions.ThermalProfileType.Definir_CGTC Then
                        Uint = ThermalProperties.CGTC_Definido
                    ElseIf ThermalProperties.TipoPerfil = ThermalEditorDefinitions.ThermalProfileType.Estimar_CGTC Then
                        Tpe = Tint
                        Uint = CalcOverallInternalHeatTransferCoefficient(holdup, L, D, DE, Me.GetRugosity(WallMaterial), Tpe, Text,
                                                                                VapVel, LiqVel, Cpl, Cpv, Kl, Kv,
                                                                                MUl, MUv, rhol, rhov)(0)
                    End If
                    If Uint <> 0.0# Then

                        DQ = (Twall - Tint) * Uint / 1000 * A
                        Uext = CalcOverallExternalHeatTransferCoefficient(D, DE, GetRugosity(WallMaterial), Tpe, Text, ThermalProperties.Incluir_isolamento)(0)

                        Dim Qwall, SR, Qrad As Double
                        Qwall = (Text - Twall) * Uext / 1000 * A

                        If ThermalProperties.IncludeSolarRadiation Then
                            If ThermalProperties.UseGlobalSolarRadiation Then
                                SR = ThermalProperties.SolarRadiationAbsorptionEfficiency * FlowSheet.FlowsheetOptions.CurrentWeather.SolarIrradiation_kWh_m2
                            Else
                                SR = ThermalProperties.SolarRadiationAbsorptionEfficiency * ThermalProperties.SolarRadiationValue_kWh_m2
                            End If
                            SR *= 3600 'kJ/m2
                            Dim Asec = Math.PI * L * DE
                            Qrad = SR / timestep * Asec 'kW
                            Qwall += Qrad
                        End If

                        WallTemperature = WallTemperature + (DQ + Qwall) / (Kwall(WallTemperature) * Math.PI * (Math.Log(DE / D) * D) * L)

                        If Double.IsNaN(DQ) Then DQ = 0.0#

                    Else

                        DQ = 0.0#
                        DQmax = 0.0#

                    End If

                    Qval = DQ

                Else

                    Qval = ThermalProperties.Calor_trocado

                End If

            Else

                If es IsNot Nothing Then Qval = es.EnergyFlow.GetValueOrDefault

            End If

            If Qval <> 0.0 Then

                If Wa > 0 Then

                    AccumulationStream.SetMassEnthalpy(Ha + Qval * timestep / Wa)

                    AccumulationStream.SpecType = StreamSpec.Pressure_and_Enthalpy

                    AccumulationStream.PropertyPackage = PropertyPackage
                    AccumulationStream.PropertyPackage.CurrentMaterialStream = AccumulationStream

                    If integrator.ShouldCalculateEquilibrium Then

                        AccumulationStream.Calculate(True, True)

                    End If

                End If

            End If

            'calculate pressure

            Dim M = AccumulationStream.GetMolarFlow()

            Dim Temperature = AccumulationStream.GetTemperature()

            Pressure = AccumulationStream.GetPressure()

            'm3/mol

            prevM = currentM

            currentM = Vol / M

            PropertyPackage.CurrentMaterialStream = AccumulationStream

            Dim LiquidVolume, RelativeLevel As Double

            If AccumulationStream.GetPressure >= Pmin Then

                If prevM = 0.0 Or integrator.ShouldCalculateEquilibrium Then

                    Dim result As IFlashCalculationResult

                    result = PropertyPackage.CalculateEquilibrium2(FlashCalculationType.VolumeTemperature, currentM, Temperature, Pressure)

                    Pressure = result.CalculatedPressure
                    Enthalpy = result.CalculatedEnthalpy

                    AccumulationStream.SetMassEnthalpy(Enthalpy)

                    AccumulationStream.SpecType = StreamSpec.Pressure_and_Enthalpy

                    LiquidVolume = AccumulationStream.Phases(1).Properties.volumetric_flow.GetValueOrDefault

                    RelativeLevel = LiquidVolume / Vol

                    SetDynamicProperty("Liquid Level", RelativeLevel * Height)

                Else

                    Pressure = currentM / prevM * Pressure

                    AccumulationStream.SpecType = StreamSpec.Temperature_and_Pressure

                End If

            Else

                Pressure = Pmin

                LiquidVolume = 0.0

                RelativeLevel = LiquidVolume / Vol

                SetDynamicProperty("Liquid Level", RelativeLevel * Height)

                AccumulationStream.SpecType = StreamSpec.Temperature_and_Pressure

            End If

            AccumulationStream.SetPressure(Pressure)

            AccumulationStream.PropertyPackage = PropertyPackage
            AccumulationStream.PropertyPackage.CurrentMaterialStream = AccumulationStream

            If integrator.ShouldCalculateEquilibrium And Pressure > 0.0 Then

                AccumulationStream.Calculate(True, True)

            End If

            SetDynamicProperty("Operating Pressure", Pressure)

            For i = 0 To 5
                If Me.GraphicObject.InputConnectors(i).IsAttached Then
                    GetInletMaterialStream(i).SetPressure(Pressure)
                End If
            Next

            Dim liqdens = AccumulationStream.Phases(1).Properties.density.GetValueOrDefault

            oms2.SetPressure(Pressure + liqdens * 9.8 * RelativeLevel * Height)

            oms1.AssignFromPhase(PhaseLabel.Vapor, AccumulationStream, False)
            oms1.AtEquilibrium = False

            If omsr IsNot Nothing Then
                omsr.AssignFromPhase(PhaseLabel.Vapor, AccumulationStream, False)
                omsr.AtEquilibrium = False
            End If

            oms2.AssignFromPhase(PhaseLabel.LiquidMixture, AccumulationStream, False)
            oms2.AtEquilibrium = False

        End Sub

        Public Overrides Sub Calculate(Optional ByVal args As Object = Nothing)

            Dim IObj As Inspector.InspectorItem = Inspector.Host.GetNewInspectorItem()

            Inspector.Host.CheckAndAdd(IObj, "", "Calculate", If(GraphicObject IsNot Nothing, GraphicObject.Tag, "Temporary Object") & " (" & GetDisplayName() & ")", GetDisplayName() & " Calculation Routine", True)

            IObj?.SetCurrent()

            IObj?.Paragraphs.Add("The separator vessel (also known as flash drum) is used to separate liquid phases from vapor in a mixed 
                                material stream.")

            IObj?.Paragraphs.Add("The separator vessel simply divides the inlet stream phases into 
                                two or three distinct streams. If the user defines values for the 
                                separation temperature and/or pressure, a TP Flash is done in the 
                                new conditions before the distribution of phases through the 
                                outlet streams.")

            If Not Me.GraphicObject.OutputConnectors(0).IsAttached Then
                Throw New Exception(FlowSheet.GetTranslatedString("Verifiqueasconexesdo"))
            ElseIf Not Me.GraphicObject.OutputConnectors(1).IsAttached Then
                Throw New Exception(FlowSheet.GetTranslatedString("Verifiqueasconexesdo"))
            End If

            Dim E0 As Double = 0.0#

            If OverrideP Or OverrideT Then CalculationMode = CalculationModes.Legacy

            Dim H, T, W, M, We, P, VF, Hf, H0 As Double, nstr As Integer
            H = 0
            T = 0
            W = 0
            We = 0
            P = 0
            VF = 0.0#

            Dim i As Integer = 1
            Dim nc As Integer = 0

            MixedStream = New MaterialStream("", "", Me.FlowSheet, Me.PropertyPackage)
            FlowSheet.AddCompoundsToMaterialStream(MixedStream)
            Dim ms As MaterialStream = Nothing

            Dim cp As IConnectionPoint

            nstr = 0.0#
            For Each cp In Me.GraphicObject.InputConnectors
                If cp.IsAttached And cp.Type = GraphicObjects.ConType.ConIn Then
                    nc += 1
                    If cp.AttachedConnector.AttachedFrom.Calculated = False Then Throw New Exception(FlowSheet.GetTranslatedString("Umaoumaiscorrentesna"))
                    ms = FlowSheet.SimulationObjects(cp.AttachedConnector.AttachedFrom.Name)
                    ms.Validate()
                    If Me.PressureCalculation = PressureBehavior.Minimum Then
                        If ms.Phases(0).Properties.pressure.GetValueOrDefault < P Then
                            P = ms.Phases(0).Properties.pressure.GetValueOrDefault
                        ElseIf P = 0 Then
                            P = ms.Phases(0).Properties.pressure.GetValueOrDefault
                        End If
                    ElseIf Me.PressureCalculation = PressureBehavior.Maximum Then
                        If ms.Phases(0).Properties.pressure.GetValueOrDefault > P Then
                            P = ms.Phases(0).Properties.pressure.GetValueOrDefault
                        ElseIf P = 0 Then
                            P = ms.Phases(0).Properties.pressure.GetValueOrDefault
                        End If
                    Else
                        P = P + ms.Phases(0).Properties.pressure.GetValueOrDefault
                        i += 1
                    End If
                    M += ms.Phases(0).Properties.molarflow.GetValueOrDefault
                    We = ms.Phases(0).Properties.massflow.GetValueOrDefault
                    W += We
                    VF += ms.Phases(2).Properties.molarfraction.GetValueOrDefault * ms.Phases(0).Properties.molarflow.GetValueOrDefault
                    If Not Double.IsNaN(ms.Phases(0).Properties.enthalpy.GetValueOrDefault) Then H += We * ms.Phases(0).Properties.enthalpy.GetValueOrDefault
                    nstr += 1
                End If
            Next

            If M <> 0.0# Then VF /= M

            H0 = H

            If Me.PressureCalculation = PressureBehavior.Average Then P = P / (i - 1)

            T = 0

            Dim n As Integer = ms.Phases(0).Compounds.Count
            Dim Vw As New Dictionary(Of String, Double)
            For Each cp In Me.GraphicObject.InputConnectors
                If cp.IsAttached And cp.Type = GraphicObjects.ConType.ConIn Then
                    ms = FlowSheet.SimulationObjects(cp.AttachedConnector.AttachedFrom.Name)
                    Dim comp As BaseClasses.Compound
                    For Each comp In ms.Phases(0).Compounds.Values
                        If Not Vw.ContainsKey(comp.Name) Then
                            Vw.Add(comp.Name, 0)
                        End If
                        Vw(comp.Name) += comp.MassFraction.GetValueOrDefault * ms.Phases(0).Properties.massflow.GetValueOrDefault
                    Next
                    If W <> 0.0# Then T += ms.Phases(0).Properties.massflow.GetValueOrDefault / W * ms.Phases(0).Properties.temperature.GetValueOrDefault
                End If
            Next

            If W = 0.0# Then T = 273.15

            CheckSpec(H, False, "enthalpy")
            CheckSpec(W, True, "mass flow")
            CheckSpec(P, True, "pressure")

            With MixedStream

                .PreferredFlashAlgorithmTag = Me.PreferredFlashAlgorithmTag

                .Phases(0).Properties.enthalpy = H
                .Phases(0).Properties.pressure = P
                .Phases(0).Properties.massflow = W
                .Phases(0).Properties.molarfraction = 1
                .Phases(0).Properties.massfraction = 1
                .Phases(2).Properties.molarfraction = VF
                Dim comp As BaseClasses.Compound
                For Each comp In .Phases(0).Compounds.Values
                    If W <> 0.0# Then comp.MassFraction = Vw(comp.Name) / W
                Next
                Dim mass_div_mm As Double = 0
                Dim sub1 As BaseClasses.Compound
                For Each sub1 In .Phases(0).Compounds.Values
                    mass_div_mm += sub1.MassFraction.GetValueOrDefault / sub1.ConstantProperties.Molar_Weight
                Next
                For Each sub1 In .Phases(0).Compounds.Values
                    If W <> 0.0# Then
                        sub1.MoleFraction = sub1.MassFraction.GetValueOrDefault / sub1.ConstantProperties.Molar_Weight / mass_div_mm
                    Else
                        sub1.MoleFraction = 0.0#
                    End If
                Next
                Me.PropertyPackage.CurrentMaterialStream = MixedStream
                MixedStream.Phases(0).Properties.temperature = T
                .Phases(0).Properties.molarflow = W / Me.PropertyPackage.AUX_MMM(PropertyPackages.Phase.Mixture) * 1000

            End With

            Select Case CalculationMode

                Case CalculationModes.Adiabatic

                    W = MixedStream.Phases(0).Properties.massflow.GetValueOrDefault

                    If nstr = 1 And E0 = 0.0# Then

                        'no need to perform flash if there's only one stream and no heat added
                        For Each cp In Me.GraphicObject.InputConnectors
                            If cp.IsAttached And cp.Type = GraphicObjects.ConType.ConIn Then
                                ms = FlowSheet.SimulationObjects(cp.AttachedConnector.AttachedFrom.Name)
                                MixedStream.Assign(ms)
                                MixedStream.AssignProps(ms)
                                Exit For
                            End If
                        Next

                    Else

                        IObj?.SetCurrent()
                        MixedStream.PropertyPackage = Me.PropertyPackage
                        MixedStream.SpecType = StreamSpec.Pressure_and_Enthalpy
                        MixedStream.Calculate(True, True)

                    End If

                    T = MixedStream.Phases(0).Properties.temperature.GetValueOrDefault

                Case CalculationModes.Legacy

                    W = MixedStream.Phases(0).Properties.massflow.GetValueOrDefault

                    If Me.OverrideP Then
                        If Not Me.GraphicObject.InputConnectors(6).IsAttached Then Throw New Exception(FlowSheet.GetTranslatedString("EnergyStreamRequired"))
                        P = Me.FlashPressure
                        MixedStream.Phases(0).Properties.pressure = P
                    Else
                        P = MixedStream.Phases(0).Properties.pressure.GetValueOrDefault
                    End If
                    If Me.OverrideT Then
                        If Not Me.GraphicObject.InputConnectors(6).IsAttached Then Throw New Exception(FlowSheet.GetTranslatedString("EnergyStreamRequired"))
                        T = Me.FlashTemperature
                        MixedStream.Phases(0).Properties.temperature = T
                    Else
                        T = MixedStream.Phases(0).Properties.temperature.GetValueOrDefault
                    End If

                    Me.PropertyPackage.CurrentMaterialStream = MixedStream

                    IObj?.SetCurrent()
                    MixedStream.PropertyPackage = Me.PropertyPackage
                    MixedStream.SpecType = StreamSpec.Temperature_and_Pressure
                    MixedStream.Calculate(True, True)

                Case CalculationModes.HeatingCoolingIsothermic

                    If Not Me.GraphicObject.InputConnectors(6).IsAttached Then Throw New Exception(FlowSheet.GetTranslatedString("EnergyStreamRequired"))
                    If Me.GraphicObject.InputConnectors(6).IsAttached Then E0 = Me.GetInletEnergyStream(6).EnergyFlow.GetValueOrDefault

                    IObj?.SetCurrent()
                    MixedStream.PropertyPackage = Me.PropertyPackage
                    MixedStream.SpecType = StreamSpec.Pressure_and_Enthalpy
                    MixedStream.Calculate(True, False)

                    T = MixedStream.Phases(0).Properties.temperature.GetValueOrDefault

                    MixedStream.SetMassEnthalpy(H + E0 / W)

                    'flash TH

                    P = MathNet.Numerics.RootFinding.Bisection.FindRootExpand(
                        Function(Px)
                            MixedStream.PropertyPackage.CurrentMaterialStream = MixedStream
                            MixedStream.SetPressure(Px)
                            MixedStream.Calculate(True, False)
                            Return MixedStream.GetTemperature() - T
                        End Function, P * 0.5, P * 2, 0.1, 100)

                    IObj?.SetCurrent()
                    MixedStream.PropertyPackage = Me.PropertyPackage
                    MixedStream.SpecType = StreamSpec.Pressure_and_Enthalpy
                    MixedStream.SetPressure(P)
                    MixedStream.Calculate(True, True)

                    T = MixedStream.Phases(0).Properties.temperature.GetValueOrDefault

                Case CalculationModes.HeatingCoolingIsobaric

                    If Not Me.GraphicObject.InputConnectors(6).IsAttached Then Throw New Exception(FlowSheet.GetTranslatedString("EnergyStreamRequired"))
                    If Me.GraphicObject.InputConnectors(6).IsAttached Then E0 = Me.GetInletEnergyStream(6).EnergyFlow.GetValueOrDefault

                    IObj?.SetCurrent()
                    MixedStream.PropertyPackage = Me.PropertyPackage
                    MixedStream.SpecType = StreamSpec.Pressure_and_Enthalpy
                    MixedStream.SetMassEnthalpy(H + E0 / W)
                    MixedStream.Calculate(True, True)

                    T = MixedStream.Phases(0).Properties.temperature.GetValueOrDefault

            End Select

            'Calculate distribution of solids into liquid outlet streams
            'Solids are distributed between liquid phases in the same ratio as the mass ratio of liquid phases
            Dim SR, VnL1(n - 1), VnL2(n - 1), VmL1(n - 1), VmL2(n - 1) As Double
            Dim HL1, HL2, W1, W2, WL1, WL2, WS As Double
            WL1 = MixedStream.Phases(3).Properties.massflow.GetValueOrDefault
            WL2 = MixedStream.Phases(4).Properties.massflow.GetValueOrDefault
            If WL2 > 0.0# Then
                SR = WL1 / (WL1 + WL2)
            Else
                SR = 1
            End If
            Dim Vids As New List(Of String)
            i = 0
            For Each comp In MixedStream.Phases(0).Compounds.Values
                VnL1(i) = MixedStream.Phases(3).Compounds(comp.Name).MolarFlow.GetValueOrDefault + SR * MixedStream.Phases(7).Compounds(comp.Name).MolarFlow.GetValueOrDefault
                VmL1(i) = MixedStream.Phases(3).Compounds(comp.Name).MassFlow.GetValueOrDefault + SR * MixedStream.Phases(7).Compounds(comp.Name).MassFlow.GetValueOrDefault
                VnL2(i) = MixedStream.Phases(4).Compounds(comp.Name).MolarFlow.GetValueOrDefault + (1 - SR) * MixedStream.Phases(7).Compounds(comp.Name).MolarFlow.GetValueOrDefault
                VmL2(i) = MixedStream.Phases(4).Compounds(comp.Name).MassFlow.GetValueOrDefault + (1 - SR) * MixedStream.Phases(7).Compounds(comp.Name).MassFlow.GetValueOrDefault
                Vids.Add(comp.Name)
                i += 1
            Next
            Dim sum1, sum2, sum3, sum4 As Double
            sum1 = VnL1.Sum
            If VnL1.Sum > 0.0# Then
                For i = 0 To VnL1.Length - 1
                    VnL1(i) /= sum1
                Next
            End If
            sum2 = VmL1.Sum
            If VmL1.Sum > 0.0# Then
                For i = 0 To VnL1.Length - 1
                    VmL1(i) /= sum2
                Next
            End If
            sum3 = VnL2.Sum
            If VnL2.Sum > 0.0# Then
                For i = 0 To VnL1.Length - 1
                    VnL2(i) /= sum3
                Next
            End If
            sum4 = VmL2.Sum
            If VmL2.Sum > 0.0# Then
                For i = 0 To VnL1.Length - 1
                    VmL2(i) /= sum4
                Next
            End If
            WL1 = MixedStream.Phases(3).Properties.massflow.GetValueOrDefault
            WL2 = MixedStream.Phases(4).Properties.massflow.GetValueOrDefault
            WS = MixedStream.Phases(7).Properties.massflow.GetValueOrDefault
            W1 = WL1 + SR * WS
            W2 = WL2 + (1 - SR) * WS
            HL1 = (WL1 * MixedStream.Phases(3).Properties.enthalpy.GetValueOrDefault + WS * SR * MixedStream.Phases(7).Properties.enthalpy.GetValueOrDefault) / (WL1 + WS * SR)
            HL2 = (WL2 * MixedStream.Phases(4).Properties.enthalpy.GetValueOrDefault + WS * (1 - SR) * MixedStream.Phases(7).Properties.enthalpy.GetValueOrDefault) / (WL2 + WS * (1 - SR))

            If Double.IsNaN(HL1) Then HL1 = 0.0#
            If Double.IsNaN(HL2) Then HL2 = 0.0#
            If Double.IsNaN(WL1) Then WL1 = 0.0#
            If Double.IsNaN(WL2) Then WL2 = 0.0#

            cp = Me.GraphicObject.OutputConnectors(0) 'vapour phase
            If cp.IsAttached Then
                ms = FlowSheet.SimulationObjects(cp.AttachedConnector.AttachedTo.Name)
                With ms
                    .Clear()
                    .ClearAllProps()
                    .SpecType = Interfaces.Enums.StreamSpec.Pressure_and_Enthalpy
                    .SetTemperature(T)
                    .SetPressure(P)
                    .SetMassEnthalpy(MixedStream.Phases(2).Properties.enthalpy.GetValueOrDefault)
                    .SetMassFlow(MixedStream.Phases(2).Properties.massflow.GetValueOrDefault)
                    Dim comp As BaseClasses.Compound
                    For Each comp In .Phases(0).Compounds.Values
                        comp.MoleFraction = MixedStream.Phases(2).Compounds(comp.Name).MoleFraction.GetValueOrDefault
                        comp.MassFraction = MixedStream.Phases(2).Compounds(comp.Name).MassFraction.GetValueOrDefault
                    Next
                    .CopyCompositions(PhaseLabel.Mixture, PhaseLabel.Vapor)
                    .Phases(2).Properties.molarfraction = 1.0
                    .AtEquilibrium = True
                End With
            End If

            'calculate liquid densities.

            PropertyPackage.CurrentMaterialStream = MixedStream

            Dim dens1 = DirectCast(PropertyPackage, PropertyPackages.PropertyPackage).AUX_LIQDENS(T, VnL1, P)
            Dim dens2 As Double = dens1

            If VnL2.Sum > 0 Then dens2 = DirectCast(PropertyPackage, PropertyPackages.PropertyPackage).AUX_LIQDENS(T, VnL2, P)

            If Double.IsNaN(dens1) Then dens1 = 0.0
            If Double.IsNaN(dens2) Then dens2 = 0.0

            If dens1 <= dens2 Then

                cp = Me.GraphicObject.OutputConnectors(1) 'liquid 1
                If cp.IsAttached Then
                    ms = FlowSheet.SimulationObjects(cp.AttachedConnector.AttachedTo.Name)
                    With ms
                        .Clear()
                        .ClearAllProps()
                        .SpecType = Interfaces.Enums.StreamSpec.Pressure_and_Enthalpy
                        .SetTemperature(T)
                        .SetPressure(P)
                        If W1 > 0.0# Then
                            .SetMassFlow(W1)
                        Else
                            .SetMassFlow(0.0)
                        End If
                        .SetMassEnthalpy(HL1)
                        Dim comp As BaseClasses.Compound
                        i = 0
                        For Each comp In .Phases(0).Compounds.Values
                            If W1 > 0 Then
                                comp.MoleFraction = VnL1(Vids.IndexOf(comp.Name))
                                comp.MassFraction = VmL1(Vids.IndexOf(comp.Name))
                            Else
                                comp.MoleFraction = MixedStream.Phases(3).Compounds(comp.Name).MoleFraction.GetValueOrDefault
                                comp.MassFraction = MixedStream.Phases(3).Compounds(comp.Name).MassFraction.GetValueOrDefault
                            End If
                            i += 1
                        Next
                        If WS = 0.0 Then
                            .CopyCompositions(PhaseLabel.Mixture, PhaseLabel.Liquid1)
                            .Phases(3).Properties.molarfraction = 1.0
                            .AtEquilibrium = True
                        End If
                    End With
                End If

                cp = Me.GraphicObject.OutputConnectors(2) 'liquid 2
                If cp.IsAttached Then
                    ms = FlowSheet.SimulationObjects(cp.AttachedConnector.AttachedTo.Name)
                    With ms
                        .Clear()
                        .ClearAllProps()
                        .SpecType = Interfaces.Enums.StreamSpec.Pressure_and_Enthalpy
                        .SetTemperature(T)
                        .SetPressure(P)
                        If W2 > 0.0# Then
                            .SetMassFlow(W2)
                        Else
                            .SetMassFlow(0.0)
                        End If
                        .SetMassEnthalpy(HL2)
                        Dim comp As BaseClasses.Compound
                        i = 0
                        For Each comp In .Phases(0).Compounds.Values
                            If W2 > 0 Then
                                comp.MoleFraction = VnL2(Vids.IndexOf(comp.Name))
                                comp.MassFraction = VmL2(Vids.IndexOf(comp.Name))
                            Else
                                comp.MoleFraction = MixedStream.Phases(4).Compounds(comp.Name).MoleFraction.GetValueOrDefault
                                comp.MassFraction = MixedStream.Phases(4).Compounds(comp.Name).MassFraction.GetValueOrDefault
                            End If
                            i += 1
                        Next
                        If WS = 0.0 Then
                            .CopyCompositions(PhaseLabel.Mixture, PhaseLabel.Liquid1)
                            .Phases(3).Properties.molarfraction = 1.0
                            .AtEquilibrium = True
                        End If
                    End With
                Else
                    If MixedStream.Phases(4).Properties.massflow.GetValueOrDefault > 0.0# Then Throw New Exception(FlowSheet.GetTranslatedString("SeparatorVessel_SecondLiquidPhaseFound"))
                End If

            Else

                cp = Me.GraphicObject.OutputConnectors(1) 'liquid 1
                If cp.IsAttached Then
                    ms = FlowSheet.SimulationObjects(cp.AttachedConnector.AttachedTo.Name)
                    With ms
                        .Clear()
                        .ClearAllProps()
                        .SpecType = Interfaces.Enums.StreamSpec.Pressure_and_Enthalpy
                        .Phases(0).Properties.temperature = T
                        .Phases(0).Properties.pressure = P
                        If W2 > 0.0# Then .Phases(0).Properties.massflow = W2 Else .Phases(0).Properties.molarflow = 0.0#
                        .Phases(0).Properties.enthalpy = HL2
                        Dim comp As BaseClasses.Compound
                        i = 0
                        For Each comp In .Phases(0).Compounds.Values
                            comp.MoleFraction = VnL2(Vids.IndexOf(comp.Name))
                            comp.MassFraction = VmL2(Vids.IndexOf(comp.Name))
                            i += 1
                        Next
                        If WS = 0.0 Then
                            .CopyCompositions(PhaseLabel.Mixture, PhaseLabel.Liquid1)
                            .Phases(3).Properties.molarfraction = 1.0
                            .AtEquilibrium = True
                        End If
                    End With
                End If

                cp = Me.GraphicObject.OutputConnectors(2) 'liquid 2
                If cp.IsAttached Then
                    ms = FlowSheet.SimulationObjects(cp.AttachedConnector.AttachedTo.Name)
                    With ms
                        .Clear()
                        .ClearAllProps()
                        .SpecType = Interfaces.Enums.StreamSpec.Pressure_and_Enthalpy
                        .Phases(0).Properties.temperature = T
                        .Phases(0).Properties.pressure = P
                        If W1 > 0.0# Then .Phases(0).Properties.massflow = W1 Else .Phases(0).Properties.molarflow = 0.0#
                        .Phases(0).Properties.enthalpy = HL1
                        Dim comp As BaseClasses.Compound
                        i = 0
                        For Each comp In .Phases(0).Compounds.Values
                            comp.MoleFraction = VnL1(Vids.IndexOf(comp.Name))
                            comp.MassFraction = VmL1(Vids.IndexOf(comp.Name))
                            i += 1
                        Next
                        If WS = 0.0 Then
                            .CopyCompositions(PhaseLabel.Mixture, PhaseLabel.Liquid1)
                            .Phases(3).Properties.molarfraction = 1.0
                            .AtEquilibrium = True
                        End If
                    End With
                Else
                    If MixedStream.Phases(3).Properties.massflow.GetValueOrDefault > 0.0# Then Throw New Exception(FlowSheet.GetTranslatedString("SeparatorVessel_SecondLiquidPhaseFound"))
                End If

            End If

            Hf = MixedStream.Phases(0).Properties.enthalpy.GetValueOrDefault * W

            Me.DeltaQ = Hf - H0

            'Energy stream - update power value (kJ/s)
            If Me.GraphicObject.InputConnectors(6).IsAttached And CalculationMode = CalculationModes.Legacy Then
                With Me.GetInletEnergyStream(6)
                    .EnergyFlow = Me.DeltaQ.GetValueOrDefault
                    .GraphicObject.Calculated = True
                End With
            End If

            'SIZING

            Me.rhol = MixedStream.Phases(1).Properties.density.GetValueOrDefault
            Me.rhov = MixedStream.Phases(2).Properties.density.GetValueOrDefault
            Me.ql = MixedStream.Phases(1).Properties.volumetric_flow.GetValueOrDefault
            Me.qv = MixedStream.Phases(2).Properties.volumetric_flow.GetValueOrDefault
            Me.wl = MixedStream.Phases(1).Properties.massflow.GetValueOrDefault
            Me.wv = MixedStream.Phases(2).Properties.massflow.GetValueOrDefault
            Me.rhoe = MixedStream.Phases(0).Properties.density.GetValueOrDefault
            Me.qe = MixedStream.Phases(0).Properties.volumetric_flow.GetValueOrDefault

            Me.C = 80
            Me.VMAX = 2
            Me.K = 0.0692
            Me.VGI = 90

            'AppendDebugLine("Sizing horizontal separator...")

            'SizeHorizontal()

            'AppendDebugLine("Sizing vertical separator...")

            'SizeVertical()

            IObj?.Close()

        End Sub

        Public Overrides Sub DeCalculate()

            Dim j As Integer = 0

            Dim cp As IConnectionPoint

            cp = Me.GraphicObject.OutputConnectors(0)
            If cp.IsAttached Then
                With Me.GetOutletMaterialStream(0)
                    .Phases(0).Properties.temperature = Nothing
                    .Phases(0).Properties.pressure = Nothing
                    .Phases(0).Properties.enthalpy = Nothing
                    Dim comp As BaseClasses.Compound
                    j = 0
                    For Each comp In .Phases(0).Compounds.Values
                        comp.MoleFraction = 0
                        comp.MassFraction = 0
                        j += 1
                    Next
                    .Phases(0).Properties.massflow = Nothing
                    .Phases(0).Properties.massfraction = 1
                    .Phases(0).Properties.molarfraction = 1
                    .GraphicObject.Calculated = False
                End With
            End If

            cp = Me.GraphicObject.OutputConnectors(1)
            If cp.IsAttached Then
                With Me.GetOutletMaterialStream(1)
                    .Phases(0).Properties.temperature = Nothing
                    .Phases(0).Properties.pressure = Nothing
                    .Phases(0).Properties.enthalpy = Nothing
                    Dim comp As BaseClasses.Compound
                    j = 0
                    For Each comp In .Phases(0).Compounds.Values
                        comp.MoleFraction = 0
                        comp.MassFraction = 0
                        j += 1
                    Next
                    .Phases(0).Properties.massflow = Nothing
                    .Phases(0).Properties.massfraction = 1
                    .Phases(0).Properties.molarfraction = 1
                    .GraphicObject.Calculated = False
                End With
            End If

        End Sub

        Function CalcOverallInternalHeatTransferCoefficient(ByVal EL As Double, ByVal L As Double,
                            ByVal Dint As Double, ByVal Dext As Double, ByVal rugosidade As Double,
                            ByVal T As Double, ByVal Text As Double, ByVal vel_g As Double, ByVal vel_l As Double,
                            ByVal Cpl As Double, ByVal Cpv As Double, ByVal kl As Double, ByVal kv As Double,
                            ByVal mu_l As Double, ByVal mu_v As Double, ByVal rho_l As Double,
                            ByVal rho_v As Double) As Double()

            If Double.IsNaN(rho_l) Then rho_l = 0.0#

            'Calculate average properties
            Dim vel As Double = vel_g + vel_l 'm/s
            Dim mu As Double = EL * mu_l + (1 - EL) * mu_v 'Pa.s
            Dim rho As Double = EL * rho_l + (1 - EL) * rho_v 'kg/m3
            Dim Cp As Double = 1000 * (EL * Cpl + (1 - EL) * Cpv) 'J/kg.K
            Dim k As Double = EL * kl + (1 - EL) * kv 'W/[m.K]
            Dim Cpmist = Cp

            'Internal HTC calculation
            Dim U_int As Double

            'Internal Re calc
            Dim Re_int = Pipe.NRe(rho, vel, Dint, mu)

            Dim epsilon = GetRugosity(WallMaterial)
            Dim ffint = 0.0#
            If Re_int > 3250 Then
                Dim a1 = Math.Log(((epsilon / Dint) ^ 1.1096) / 2.8257 + (7.149 / Re_int) ^ 0.8961) / Math.Log(10.0#)
                Dim b1 = -2 * Math.Log((epsilon / Dint) / 3.7065 - 5.0452 * a1 / Re_int) / Math.Log(10.0#)
                ffint = (1 / b1) ^ 2
            Else
                ffint = 64 / Re_int
            End If

            'Internal Pr calc
            Dim Pr_int = Pipe.NPr(Cp, mu, k)

            'Internal h calc
            Dim h_int = Pipe.hint_petukhov(k, Dint, ffint, Re_int, Pr_int)

            'Internal h contribution
            U_int = h_int

            'Pipe wall HTC contribution
            Dim U_parede = 0.0#

            U_parede = Kwall(T) / (Math.Log(Dext / Dint) * Dint)
            If Dext = Dint Then U_parede = 0.0#

            'Calculate overall HTC
            Dim _U As Double

            If U_int <> 0.0# Then
                _U = _U + 1 / U_int
            Else
                _U = _U + 1.0E+30
            End If
            If U_parede <> 0.0# Then
                _U = _U + 1 / U_parede
            Else
                _U = _U + 1.0E+30
            End If

            Return New Double() {1 / _U, U_int, U_parede} '[W/m².K]

        End Function

        Function CalcOverallExternalHeatTransferCoefficient(Dint As Double, Dext As Double, rugosidade As Double,
                            T As Double, Text As Double, isolamento As Boolean) As Double()

            'Pipe wall HTC contribution
            Dim U_parede = 0.0#

            U_parede = Kwall(T) / (Math.Log(Dext / Dint) * Dint)
            If Dext = Dint Then U_parede = 0.0#

            'Insulation HTC contribution
            Dim U_isol = 0.0#

            Dim esp_isol = 0.0#
            If isolamento = True Then

                esp_isol = ThermalProperties.Espessura 'm
                U_isol = ThermalProperties.Condtermica / (Math.Log((Dext + 2 * esp_isol) / Dext) * Dext)

            End If

            'External HTC contribution
            Dim U_ext = 0.0#

            Dim mu2, k2, cp2, rho2 As Double 'Soil, undergound

            'Average air properties

            Dim Pext As Double = 101325.0

            Dim vel = Convert.ToDouble(ThermalProperties.Velocidade)

            Dim props = Pipe.PropsAR(Text, Pext)
            mu2 = props(1)
            rho2 = props(0)
            cp2 = props(2) * 1000
            k2 = props(3)

            'External Re
            Dim Re_ext = Pipe.NRe(rho2, vel, (Dext + 2 * esp_isol), mu2)

            'External Pr
            Dim Pr_ext = Pipe.NPr(cp2, mu2, k2)

            'External h
            Dim h_ext = Pipe.hext_holman(k2, (Dext + 2 * esp_isol), Re_ext, Pr_ext)

            'External HTC contribution
            U_ext = h_ext * (Dext + 2 * esp_isol) / Dint

            'Calculate overall HTC
            Dim _U As Double

            If U_parede <> 0.0# Then
                _U = _U + 1 / U_parede
            Else
                _U = _U + 1.0E+30
            End If
            If U_isol <> 0.0# Then
                _U = _U + 1 / U_isol
            Else
                If isolamento = True Then
                    _U = _U + 1.0E+30
                End If
            End If
            If U_ext <> 0.0# Then
                _U = _U + 1 / U_ext
            Else
                _U = _U + 1.0E+30
            End If

            Return New Double() {1 / _U, U_parede, U_isol, U_ext} '[W/m².K]

        End Function


        Function Kwall(ByVal T As Double) As Double

            Dim kp As Double

            Select Case WallMaterial
                Case "Steel"
                    kp = -0.000000004 * T ^ 3 - 0.00002 * T ^ 2 + 0.021 * T + 33.743
                Case "Carbon Steel"
                    kp = 0.000000007 * T ^ 3 - 0.00002 * T ^ 2 - 0.0291 * T + 70.765
                Case "Cast Iron"
                    kp = -0.00000008 * T ^ 3 + 0.0002 * T ^ 2 - 0.211 * T + 127.99
                Case "Stainless Steel"
                    kp = 14.6 + 0.0127 * (T - 273.15)
                Case "Commercial Copper"
                    kp = 420.75 - 0.068493 * T
            End Select

            Return kp   'W/m.K

        End Function

        Public Function GetRugosity(ByVal material As String) As Double

            Dim epsilon As Double

            'wall rugosity in m

            Select Case material
                Case "Steel"
                    epsilon = 0.0000457
                Case "Carbon Steel"
                    epsilon = 0.000045
                Case "Cast Iron"
                    epsilon = 0.000259
                Case "Stainless Steel"
                    epsilon = 0.000045
                Case "Commercial Copper"
                    epsilon = 0.0000015
            End Select

            Return epsilon

        End Function

        Public Overrides Function GetPropertyValue(ByVal prop As String, Optional ByVal su As Interfaces.IUnitsOfMeasure = Nothing) As Object

            Dim val0 As Object = MyBase.GetPropertyValue(prop, su)

            If Not val0 Is Nothing Then

                Return val0

            Else

                If su Is Nothing Then su = New SystemsOfUnits.SI
                Dim cv As New SystemsOfUnits.Converter
                Dim value As Double = 0
                Dim propidx As Integer = Convert.ToInt32(prop.Split("_")(2))

                Select Case propidx

                    Case 0
                        'PROP_SV_0	Separation Temperature
                        value = SystemsOfUnits.Converter.ConvertFromSI(su.temperature, Me.FlashTemperature)
                    Case 1
                        'PROP_SV_1	Separation Pressure
                        value = SystemsOfUnits.Converter.ConvertFromSI(su.pressure, Me.FlashPressure)

                End Select

                Return value

            End If

        End Function

        Public Overloads Overrides Function GetProperties(ByVal proptype As Interfaces.Enums.PropertyType) As String()
            Dim i As Integer = 0
            Dim proplist As New ArrayList
            Dim basecol = MyBase.GetProperties(proptype)
            If basecol.Length > 0 Then proplist.AddRange(basecol)
            Select Case proptype
                Case PropertyType.RW
                    For i = 0 To 1
                        proplist.Add("PROP_SV_" + CStr(i))
                    Next
                Case PropertyType.WR
                    For i = 0 To 1
                        proplist.Add("PROP_SV_" + CStr(i))
                    Next
                Case PropertyType.ALL
                    For i = 0 To 1
                        proplist.Add("PROP_SV_" + CStr(i))
                    Next
            End Select
            Return proplist.ToArray(GetType(System.String))
            proplist = Nothing
        End Function

        Public Overrides Function SetPropertyValue(ByVal prop As String, ByVal propval As Object, Optional ByVal su As Interfaces.IUnitsOfMeasure = Nothing) As Boolean

            If MyBase.SetPropertyValue(prop, propval, su) Then Return True

            If su Is Nothing Then su = New SystemsOfUnits.SI
            Dim cv As New SystemsOfUnits.Converter
            Dim propidx As Integer = Convert.ToInt32(prop.Split("_")(2))

            Select Case propidx
                Case 0
                    'PROP_SV_0	Separation Temperature
                    Me.FlashTemperature = SystemsOfUnits.Converter.ConvertToSI(su.temperature, propval)
                Case 1
                    'PROP_SV_1	Separation Pressure
                    Me.FlashPressure = SystemsOfUnits.Converter.ConvertToSI(su.pressure, propval)
            End Select
            Return 1
        End Function

        Public Overrides Function GetPropertyUnit(ByVal prop As String, Optional ByVal su As Interfaces.IUnitsOfMeasure = Nothing) As String

            Dim u0 As String = MyBase.GetPropertyUnit(prop, su)

            If u0 = "NF" Then

                If su Is Nothing Then su = New SystemsOfUnits.SI
                Dim cv As New SystemsOfUnits.Converter
                Dim value As String = ""
                Dim propidx As Integer = Convert.ToInt32(prop.Split("_")(2))

                Select Case propidx

                    Case 0
                        'PROP_SV_0	Separation Temperature
                        value = su.temperature
                    Case 1
                        'PROP_SV_1	Separation Pressure
                        value = su.pressure

                End Select

                Return value

            Else

                Return u0

            End If

        End Function

        Public Overrides Sub DisplayEditForm()

            If f Is Nothing Then
                f = New EditingForm_Vessel With {.VesselObject = Me}
                f.ShowHint = GlobalSettings.Settings.DefaultEditFormLocation
                f.Tag = "ObjectEditor"
                Me.FlowSheet.DisplayForm(f)
            Else
                If f.IsDisposed Then
                    f = New EditingForm_Vessel With {.VesselObject = Me}
                    f.ShowHint = GlobalSettings.Settings.DefaultEditFormLocation
                    f.Tag = "ObjectEditor"
                    Me.FlowSheet.DisplayForm(f)
                Else
                    f.Activate()
                End If
            End If

        End Sub

        Public Overrides Sub UpdateEditForm()
            If f IsNot Nothing Then
                If Not f.IsDisposed Then
                    f.UIThread(Sub() f.UpdateInfo())
                End If
            End If
        End Sub

        Public Overrides Function GetIconBitmap() As Object
            Return My.Resources.separator
        End Function

        Public Overrides Function GetIconBitmapBytes() As Byte()

            Return GetBytesFromResource("DWSIM.UnitOperations.separator.png")

        End Function

        Public Overrides Function GetDisplayDescription() As String
            Return ResMan.GetLocalString("VESSEL_Desc")
        End Function

        Public Overrides Function GetDisplayName() As String
            Return ResMan.GetLocalString("VESSEL_Name")
        End Function

        Public Overrides Sub CloseEditForm()
            If f IsNot Nothing Then
                If Not f.IsDisposed Then
                    f.Close()
                    f = Nothing
                End If
            End If
        End Sub

        Public Overrides ReadOnly Property MobileCompatible As Boolean
            Get
                Return True
            End Get
        End Property

        Public Sub SizeVertical()

            Try

                Dim qv As Double = Me.qv * SurgeFactor
                Dim ql As Double = Me.ql * SurgeFactor

                Dim tres As Double = ResidenceTime

                Dim rho_ml As Double = Me.rhol
                Dim rho_ns As Double = Me.rhoe

                Dim vk As Double = Me.K * ((rho_ml - Me.rhov) / Me.rhov) ^ 0.5
                Dim vp As Double = Me.VGI / 100 * vk
                Dim At As Double = qv / vp

                Dim dmin As Double = (4 * At / Math.PI) ^ 0.5
                Dim lmin As Double = DimensionRatio * dmin

                'bocal de entrada
                Dim vmaxbe As Double = Me.C / (rho_ns) ^ 0.5
                Dim aminbe As Double = (qv + ql) / (vmaxbe)
                Dim dminbe As Double = (4 * aminbe / Math.PI) ^ 0.5

                'bocal de gas
                Dim vmaxbg As Double = Me.C / (Me.rhov) ^ 0.5
                Dim aminbg As Double = (qv) / (vmaxbg)
                Dim dminbg As Double = (4 * aminbg / Math.PI) ^ 0.5

                'bocal de liquido
                Dim vmaxbl As Double = Me.VMAX
                Dim aminbl2 As Double = (ql) / (vmaxbl)
                Dim dminbl As Double = (4 * aminbl2 / Math.PI) ^ 0.5

                BSLV = dminbl
                BSGV = dminbg
                BeV = dminbe

                DV = dmin
                AV = lmin

            Catch ex As Exception

            End Try

        End Sub

        Public Sub SizeHorizontal()

            Try

                Dim qv As Double = Me.qv * SurgeFactor
                Dim ql As Double = Me.ql * SurgeFactor

                Dim rho_ml As Double = Me.rhol
                Dim rho_ns As Double = Me.rhoe

                Dim x, y, l_d, dv, dl, vl1, vl2, cv As Double

                Dim vk As Double = Me.K * ((rho_ml - Me.rhov) / Me.rhov) ^ 0.5
                Dim vp As Double = Me.VGI / 100 * vk

                'bocal de entrada
                Dim vmaxbe As Double = Me.C / (rho_ns) ^ 0.5
                Dim aminbe As Double = (qv + ql) / (vmaxbe)
                Dim dminbe As Double = (4 * aminbe / Math.PI) ^ 0.5

                'bocal de gas
                Dim vmaxbg As Double = Me.C / (Me.rhov) ^ 0.5
                Dim aminbg As Double = (qv) / (vmaxbg)
                Dim dminbg As Double = (4 * aminbg / Math.PI) ^ 0.5

                'bocal de liquido
                Dim vmaxbl As Double = Me.VMAX
                Dim aminbl2 As Double = (ql) / (vmaxbl)
                Dim dminbl As Double = (4 * aminbl2 / Math.PI) ^ 0.5

                'vaso
                Dim tr As Double = ResidenceTime

                l_d = DimensionRatio

                x = 0.01
                Do
                    y = (1 / Math.PI) * Math.Acos(1 - 2 * x) - (2 / Math.PI) * (1 - 2 * x) * (x * (1 - x)) ^ 0.5
                    dv = (4 / Math.PI * qv / (vp)) ^ 0.5 * ((x / y) / l_d) ^ 0.5
                    dl = ((4 / (Math.PI * l_d)) * (ql) * Convert.ToDouble(tr * 60) / (1 - y)) ^ (1 / 3)
                    x += 0.0001
                Loop Until Math.Abs(dv - dl) < 0.0001 Or x >= 0.5
                vl1 = (ql) * tr / (1 / 60)
                vl2 = (1 - y) * Math.PI * dl ^ 3 / 4 * l_d
                Dim cnt As Integer = 0
                If vl2 < vl1 Then
                    Do
                        vl2 = (1 - y) * Math.PI * dl ^ 3 / 4 * l_d
                        dl = dl * 1.001
                        cnt += 1
                    Loop Until Math.Abs(vl2 - vl1) < 0.001 Or cnt > 100
                End If

                Dim diam As Double
                If dl > dv Then diam = dl
                If dv > dl Then diam = dv

                cv = l_d * diam

                BSLH = dminbl
                BSGH = dminbg
                BeH = dminbe

                DH = diam
                AH = cv

            Catch ex As Exception

            End Try

        End Sub

        Public Overrides Function GetReport(su As IUnitsOfMeasure, ci As Globalization.CultureInfo, numberformat As String) As String

            Dim str As New Text.StringBuilder

            Dim istr As MaterialStream
            istr = Me.GetInletMaterialStream(0)
            istr.PropertyPackage.CurrentMaterialStream = istr

            str.AppendLine("Gas/Liquid Separator: " & Me.GraphicObject.Tag)
            str.AppendLine("Property Package: " & Me.PropertyPackage.ComponentName)
            str.AppendLine()
            str.AppendLine("Inlet conditions (First Stream)")
            str.AppendLine()
            str.AppendLine("    Temperature: " & SystemsOfUnits.Converter.ConvertFromSI(su.temperature, istr.Phases(0).Properties.temperature.GetValueOrDefault).ToString(numberformat, ci) & " " & su.temperature)
            str.AppendLine("    Pressure: " & SystemsOfUnits.Converter.ConvertFromSI(su.pressure, istr.Phases(0).Properties.pressure.GetValueOrDefault).ToString(numberformat, ci) & " " & su.pressure)
            str.AppendLine("    Total mass flow: " & SystemsOfUnits.Converter.ConvertFromSI(su.massflow, istr.Phases(0).Properties.massflow.GetValueOrDefault).ToString(numberformat, ci) & " " & su.massflow)
            str.AppendLine("    Total volumetric flow: " & SystemsOfUnits.Converter.ConvertFromSI(su.volumetricFlow, istr.Phases(0).Properties.volumetric_flow.GetValueOrDefault).ToString(numberformat, ci) & " " & su.volumetricFlow)
            str.AppendLine("    Vapor fraction: " & istr.Phases(2).Properties.molarfraction.GetValueOrDefault.ToString(numberformat, ci))
            str.AppendLine("    Vapor mass flow: " & SystemsOfUnits.Converter.ConvertFromSI(su.massflow, istr.Phases(1).Properties.massflow.GetValueOrDefault).ToString(numberformat, ci) & " " & su.massflow)
            str.AppendLine("    Vapor volumetric flow: " & SystemsOfUnits.Converter.ConvertFromSI(su.volumetricFlow, istr.Phases(1).Properties.volumetric_flow.GetValueOrDefault).ToString(numberformat, ci) & " " & su.volumetricFlow)
            str.AppendLine("    Liquid mass flow: " & SystemsOfUnits.Converter.ConvertFromSI(su.massflow, istr.Phases(2).Properties.massflow.GetValueOrDefault).ToString(numberformat, ci) & " " & su.massflow)
            str.AppendLine("    Liquid volumetric flow: " & SystemsOfUnits.Converter.ConvertFromSI(su.volumetricFlow, istr.Phases(2).Properties.volumetric_flow.GetValueOrDefault).ToString(numberformat, ci) & " " & su.volumetricFlow)
            str.AppendLine("    Compounds: " & istr.PropertyPackage.RET_VNAMES.ToArrayString)
            str.AppendLine("    Molar composition: " & istr.PropertyPackage.RET_VMOL(PropertyPackages.Phase.Mixture).ToArrayString(ci))
            str.AppendLine()
            str.AppendLine("Sizing parameters")
            str.AppendLine()
            str.AppendLine("    L/D ratio: " & DimensionRatio.ToString(numberformat, ci))
            str.AppendLine("    Liquid residence time: " & SystemsOfUnits.Converter.ConvertFromSI(su.time, ResidenceTime * 60).ToString(numberformat, ci) & " " & su.time)
            str.AppendLine("    Surge factor: " & SurgeFactor.ToString(numberformat, ci))
            str.AppendLine()
            str.AppendLine("Sizing results - vertical separator")
            str.AppendLine()
            str.AppendLine("    Inlet noozle diameter: " & SystemsOfUnits.Converter.ConvertFromSI(su.diameter, BeV).ToString(numberformat, ci) & " " & su.diameter)
            str.AppendLine("    Outlet gas noozle diameter: " & SystemsOfUnits.Converter.ConvertFromSI(su.diameter, BSGV).ToString(numberformat, ci) & " " & su.diameter)
            str.AppendLine("    Outlet liquid noozle diameter: " & SystemsOfUnits.Converter.ConvertFromSI(su.diameter, BSLV).ToString(numberformat, ci) & " " & su.diameter)
            str.AppendLine("    Separator diameter: " & SystemsOfUnits.Converter.ConvertFromSI(su.diameter, DV).ToString(numberformat, ci) & " " & su.diameter)
            str.AppendLine("    Separator height: " & SystemsOfUnits.Converter.ConvertFromSI(su.diameter, AV).ToString(numberformat, ci) & " " & su.diameter)
            str.AppendLine()
            str.AppendLine("Sizing results - horizontal separator")
            str.AppendLine()
            str.AppendLine("    Inlet noozle diameter: " & SystemsOfUnits.Converter.ConvertFromSI(su.diameter, BeH).ToString(numberformat, ci) & " " & su.diameter)
            str.AppendLine("    Outlet gas noozle diameter: " & SystemsOfUnits.Converter.ConvertFromSI(su.diameter, BSGH).ToString(numberformat, ci) & " " & su.diameter)
            str.AppendLine("    Outlet liquid noozle diameter: " & SystemsOfUnits.Converter.ConvertFromSI(su.diameter, BSLH).ToString(numberformat, ci) & " " & su.diameter)
            str.AppendLine("    Separator diameter: " & SystemsOfUnits.Converter.ConvertFromSI(su.diameter, DH).ToString(numberformat, ci) & " " & su.diameter)
            str.AppendLine("    Separator length: " & SystemsOfUnits.Converter.ConvertFromSI(su.diameter, AH).ToString(numberformat, ci) & " " & su.diameter)

            Return str.ToString

        End Function

        Public Overrides Function GetPropertyDescription(p As String) As String
            If p.Equals("Override Separation Pressure") Then
                Return "[Legacy mode only] Overrides the separation pressure. Enabling this setting requires an energy stream connected to the separator."
            ElseIf p.Equals("Separation Pressure") Then
                Return "[Legacy mode only] If the separation pressure is overriden, enter the desired value."
            ElseIf p.Equals("Override Separation Temperature") Then
                Return "[Legacy mode only] Overrides the separation temperature. Enabling this setting requires an energy stream connected to the separator."
            ElseIf p.Equals("Separation Temperature") Then
                Return "[Legacy mode only] If the separation temperature is overriden, enter the desired value."
            Else
                Return p
            End If
        End Function

    End Class

End Namespace
