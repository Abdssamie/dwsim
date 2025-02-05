Public Interface IConvergenceHelperTrainingData

    Property RequestType As ConvergenceHelperRequestType

    Property Reactions As List(Of IReaction)

    Property ModelName As String

    Property NumberOfCompounds As Integer

    Property CompoundNames As String()

    Property Temperature As String

    Property Temperature2 As String

    Property Pressure As String

    Property MassEnthalpy As String

    Property MassEntropy As String

    Property VaporMolarFraction As String

    Property MixtureMolarFlows As String()

    Property MixtureMolarFlows2 As String()

    Property VaporMolarFlows As String()

    Property Liquid1MolarFlows As String()

    Property Liquid2MolarFlows As String()

    Property SolidMolarFlows As String()

    Property KValuesVL1 As String()

    Property KValuesVL2 As String()

    Property ReactionExtents As String()

End Interface

Public Interface IConvergenceHelperRequest

    Property RequestType As ConvergenceHelperRequestType

    Property ModelName As String

    Property NumberOfCompounds As Integer

    Property CompoundNames As String()

    Property Temperature As Double?

    Property Pressure As Double?

    Property MassEnthalpy As Double?

    Property MassEntropy As Double?

    Property VaporMolarFraction As Double?

    Property MixtureMolarFlows As Double()

End Interface

Public Interface IConvergenceHelperResponse

    Property RequestType As ConvergenceHelperRequestType

    Property MetaData As IConvergenceHelperMetaData

    Property ModelName As String

    Property IsValid As Boolean

    Property Reason As String

    Property InnerException As Exception

    Property Temperature As Double?

    Property Temperature2 As Double?

    Property Pressure As Double?

    Property MassEnthalpy As Double?

    Property MassEntropy As Double?

    Property VaporMolarFraction As Double?

    Property MixtureMolarFlows As Double()

    Property VaporMolarFlows As Double()

    Property Liquid1MolarFlows As Double()

    Property Liquid2MolarFlows As Double()

    Property SolidMolarFlows As Double()

    Property KValuesVL1 As Double()

    Property KValuesVL2 As Double()

    Property MixtureMolarFlows2 As Double()

    Property ReactionExtents As Double()

End Interface

Public Interface IConvergenceHelperMetaData

    Property ModelName As String

    Property PropertyPackageName As String
    Property CreatedOn As DateTime

    Property LastUpdatedOn As DateTime

    Property NumberOfSamples As Integer

    Property NumberOfCompounds As Integer

    Property NumberOfReactions As Integer

    Property CompoundNames As String()

    Property TemperatureRange As Single()

    Property PressureRange As Single()

    Property MassEnthalpyRange As Single()

    Property MassEntropyRange As Single()

    Property VaporMolarFractionRange As Single()

    Property MolarCompositionRange As List(Of Single())

    Property TrainingDataMSE As Single

    Property TestingDataMSE As Single

End Interface

Public Enum ConvergenceHelperRequestType

    PVFlash = 0
    TVFlash = 1
    PTFlash = 2
    PHFlash = 3
    PSFlash = 4

    GibbsReactorIsothermic = 5
    GibbsReactorAdiabatic = 6

    EquilibriumReactorIsothermic = 8
    EquilibriumReactorAdiabatic = 9

End Enum