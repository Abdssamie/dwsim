Imports System.Text
Imports System.IO

' MIGRATION: Removed TextBoxStreamWriter (System.Windows.Forms.TextBox dependency).
' TextBox-based console redirection is a UI concern — out of scope for the headless engine.

Namespace ConsoleRedirection

    Public Class InspectorItemStreamWriter

        Inherits TextWriter

        Private _IObj As Inspector.InspectorItem

        Private paragraph As String = ""

        Public Sub New(ByVal IObj As Inspector.InspectorItem)
            _IObj = IObj
        End Sub

        Public Sub SetIObj(IObj As Inspector.InspectorItem)
            _IObj = IObj
        End Sub

        Public Overrides Sub Write(value As String)
            MyBase.Write(value)
            _IObj?.Paragraphs.Add(value)
        End Sub

        Public Overrides Sub Write(ByVal value As Char)
            MyBase.Write(value)
            If value <> vbCr And value <> vbLf And value <> vbCrLf Then
                paragraph += value
            Else
                _IObj?.Paragraphs.Add(paragraph)
                paragraph = ""
            End If
        End Sub

        Public Overrides ReadOnly Property Encoding() As Encoding
            Get
                Return System.Text.Encoding.UTF8
            End Get
        End Property

    End Class

End Namespace
