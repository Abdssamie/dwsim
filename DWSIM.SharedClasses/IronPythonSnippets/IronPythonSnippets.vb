Imports DWSIM.Interfaces
Imports System.Reflection

Namespace IronPythonSnippets

    Public Class IronPythonSnippet
        Public Property Name As String
        Public Property Description As String
        Public Property Code As String
    End Class

    Public Class IronPythonSnippets

        Public Shared Function GetSnippets() As List(Of IronPythonSnippet)

            Dim list As New List(Of IronPythonSnippet)

            Dim stream As IO.Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("DWSIM.SharedClasses.IronPythonSnippets.xml")

            If stream IsNot Nothing Then
                Dim doc As New Xml.XmlDocument
                doc.Load(stream)

                For Each node As Xml.XmlNode In doc.SelectNodes("IronPythonSnippets/Snippet")
                    Dim snip As New IronPythonSnippet
                    snip.Name = node.Attributes("Name").Value
                    snip.Description = node.Attributes("Description").Value
                    snip.Code = node.InnerText
                    list.Add(snip)
                Next
            End If

            Return list

        End Function

        Public Shared Sub PopulateWithDynamicSnippets(contextmenu As Object, fs As IFlowsheet, InsertText As Action(Of String))
            ' UI Logic removed for Pure Headless mode
        End Sub

    End Class

End Namespace
