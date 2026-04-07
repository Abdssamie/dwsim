Public Interface IUnitOperation

    Property Dimensions As List(Of IDimension)

    Property SelectedEquipmentType As String

    ReadOnly Property EquipmentTypes As List(Of String)

    Property AttachedExtensions As List(Of IUnitOperationExtension)

End Interface

Public Interface IUnitOperationExtension

    Property Name As String

    Property Description As String

    Property Author As String

    Property Website As String

    Function NewInstance() As IUnitOperationExtension

    Sub Run(UnitOperation As IUnitOperation)

    Sub ReleaseResources()

End Interface
