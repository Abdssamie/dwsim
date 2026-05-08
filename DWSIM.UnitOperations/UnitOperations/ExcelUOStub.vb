Imports DWSIM.Interfaces
Imports DWSIM.Interfaces.Enums
Imports DWSIM.SharedClasses.UnitOperations

Namespace UnitOperations

    <System.Serializable()> Public Class ExcelUO
        Inherits UnitOpBaseClass

        Public Overrides Property ObjectClass As SimulationObjectClass = SimulationObjectClass.UserModels

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(name As String, description As String)
            MyBase.New()
            Me.ComponentName = name
            Me.ComponentDescription = description
        End Sub

        Public Overrides Sub Calculate(Optional args As Object = Nothing)
            ' Do nothing in headless mode
        End Sub

        Public Overrides Function CloneXML() As Object
            Return Nothing
        End Function

        Public Overrides Function CloneJSON() As Object
            Return Nothing
        End Function

        Public Overrides ReadOnly Property MobileCompatible As Boolean
            Get
                Return True
            End Get
        End Property

        Public Property InputParams As New Dictionary(Of String, Object)
        Public Property OutputParams As New Dictionary(Of String, Object)
        Public Property FileIsEmbedded As Boolean = False
        Public Property EmbeddedFileName As String = ""

    End Class

End Namespace
