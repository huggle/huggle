Imports System
Imports System.Drawing
Imports System.Xml

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLBRElement.
    ''' </summary>
    Public Class HTMLBRElement
        Inherits HTMLElement
        Public Sub New(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
            Initialize(document, parent, xnode)
        End Sub
        
        Public Overloads Overrides Sub Layout(renderer As HTMLRenderer, g As Graphics)
            renderer.NewBand()
            renderer.NewBand()
        End Sub
    End Class
End Namespace
