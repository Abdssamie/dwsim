Namespace Global.Inspector
    Public Module Methods
        Public Sub SetCurrent(Optional obj As Object = Nothing)
        End Sub
    End Module

    Public Class Host
        Public Shared Property CurrentSolutionID As Long
        Public Shared Function GetNewInspectorItem() As InspectorItem
            Return New InspectorItem()
        End Function
        Public Shared Sub CheckAndAdd(item As InspectorItem, ParamArray args As Object())
        End Sub
    End Class

    Public Class InspectorItem
        Public Property Paragraphs As New List(Of String)
        Public Sub Close()
        End Sub
        Public Sub SetCurrent(Optional obj As Object = Nothing)
        End Sub
        Public Shared Function GetImageHTML(ParamArray args As Object()) As String
            Return ""
        End Function
    End Class
End Namespace

' Global level stubs for direct usage without Inspector. prefix
Public Class InspectorItem
    Inherits Global.Inspector.InspectorItem
End Class

Public Class Host
    Inherits Global.Inspector.Host
End Class

' This also allows Inspector.SetCurrent and Inspector.InspectorItem if used as class
Public Class Inspector
    Public Shared Sub SetCurrent(Optional obj As Object = Nothing)
    End Sub

    Public Class Host
        Inherits Global.Inspector.Host
    End Class

    Public Class InspectorItem
        Inherits Global.Inspector.InspectorItem
    End Class
End Class

Namespace Global.Eto.Forms
    Public Class ContextMenu
    End Class
    Public Class ButtonMenuItem
    End Class
End Namespace

Namespace Global.DWSIM.UI.Shared.Common
    Public Class FormUtils
    End Class
End Namespace

Namespace Global.DWSIM.UI.Inspector
    Public Class Host
        Inherits Global.Inspector.Host
    End Class
End Namespace
