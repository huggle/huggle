Imports System.Xml

Namespace Anole

    Public Class HTMLParser

        Private _Document As New XmlDocument

        Public Sub Parse(ByVal Document As Anole.HTMLDocument)
            Try
                Dim s As String = Document.HTML.Replace("<br>", "<br/>").Replace("<hr>", "<hr/>")
                _Document.LoadXml(s)

            Catch ex As Exception

            End Try
        End Sub

    End Class

End Namespace