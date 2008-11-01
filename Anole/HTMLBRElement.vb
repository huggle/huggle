Namespace Anole

    Public Class HTMLBRElement : Inherits HTMLElement

        Public Sub New(ByRef document As HTMLDocument, ByVal parent As HTMLElement, ByVal xnode As XmlNode)
            Initialize(document, parent, xnode)
        End Sub

        Public Overloads Overrides Sub Layout(ByVal renderer As HTMLRenderer, ByVal g As Graphics)
            renderer.NewBand()
            renderer.NewBand()
        End Sub
    End Class

End Namespace
