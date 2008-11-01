Namespace Anole

    Public Class HTMLCenterElement : Inherits HTMLElement

        Public Sub New(ByRef document As HTMLDocument, ByVal parent As HTMLElement, ByVal xnode As XmlNode)
            Initialize(document, parent, xnode)
            Me.Alignment = HTMLAlignment.Center
            ParseChildren(xnode)
        End Sub

        Public Overloads Overrides Sub Layout(ByVal renderer As HTMLRenderer, ByVal g As Graphics)
            'If the current band has items, then start a new band.
            If renderer.CurrentBand().Width > 0 Then renderer.NewBand()

            MyBase.Layout(renderer, g)
            renderer.NewBand()
        End Sub

    End Class

End Namespace
