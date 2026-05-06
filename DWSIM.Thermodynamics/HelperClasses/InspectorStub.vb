' MIGRATION STUB: Headless Inspector Shim for DWSIM.Thermodynamics
' This file provides a no-op implementation of the Inspector.Host and
' Inspector.InspectorItem types so that the engine layer compiles without
' a dependency on the WinForms-based DWSIM.Inspector project.
' All methods are intentionally empty; runtime inspection is a UI concern.

Namespace Inspector

    ''' <summary>
    ''' No-op inspector item for headless/engine operation.
    ''' </summary>
    Public Class InspectorItem

        Public Property Name As String = ""
        Public Property Description As String = ""
        Public Property CodePath As String = ""
        Public Property Items As New List(Of InspectorItem)
        Public Property Paragraphs As New List(Of String)

        ''' <summary>Null-safe paragraph adder — swallowed in headless mode.</summary>
        Public Sub Add(ByVal text As String)
            ' no-op in headless mode
        End Sub

        Public Sub SetCurrent()
            ' no-op in headless mode
        End Sub

        Public Sub Close()
            ' no-op in headless mode
        End Sub

    End Class

    ''' <summary>
    ''' No-op inspector host for headless/engine operation.
    ''' </summary>
    Public Class Host

        Public Shared Items As New List(Of InspectorItem)
        Public Shared CurrentItem As InspectorItem = Nothing

        Public Shared Sub SetCurrent(ii As InspectorItem)
            ' no-op in headless mode
        End Sub

        ''' <summary>Always returns Nothing in headless mode.</summary>
        Public Shared Function GetNewInspectorItem(
                Optional memberName As String = "",
                Optional fileName As String = "",
                Optional lineNumber As Integer = 0) As InspectorItem
            Return Nothing
        End Function

        Public Shared Sub CheckAndAdd(ii As InspectorItem, callingmethod As String, method As String,
                                      name As String, description As String, Optional current As Boolean = False)
            ' no-op in headless mode
        End Sub

        Public Shared Function GetItemAndChildren(ii As InspectorItem) As List(Of InspectorItem)
            Return New List(Of InspectorItem)()
        End Function

    End Class

End Namespace
