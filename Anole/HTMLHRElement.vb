Imports System
Imports System.Drawing
Imports System.Xml

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLHRElement.
    ''' </summary>
    Public Class HTMLHRElement
        Inherits HTMLElement
        Public Sub New(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
                'TBD: store information attributes about the
                'horizontal rule: line color, thickness, etc.
            Initialize(document, parent, xnode)
        End Sub
        
        Public Overloads Overrides Sub Layout(renderer As HTMLRenderer, g As Graphics)
            Dim band As HTMLRenderBand
            band = renderer.NewBand()
            band.AddItem(New HTMLRenderHRItem(renderer.TargetWidth, Me))
            renderer.NewBand()
        End Sub
        
        
    End Class
End Namespace
