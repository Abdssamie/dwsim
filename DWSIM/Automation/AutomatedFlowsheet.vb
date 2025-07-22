Imports DWSIM.Automation.UI.Classic.Panels

Namespace Automation.UI.Classic

    Public Class AutomatedFlowsheet

        Public Property Flowsheet As FormFlowsheet
        Public Property Charts As New PanelCharts
        Public Property DynamicsManager As New PanelDynamicsIntegrator
        Public Property DynamicsIntegrator As New PanelDynamicsIntegrator
        Public Property PFD As New PanelFlowsheetPFD
        Public Property Spreadsheet As New PanelSpreadsheet
        Public Property Settings As New PanelSimulationSettings
        Public Property MaterialStreams As New PanelMaterialStreams
        Public Property SensitivityAnalysis As New PanelSensitivityAnalysis
        Public Property Optimization As New PanelOptimization
        Public Property Reports As New PanelReports

    End Class

End Namespace
