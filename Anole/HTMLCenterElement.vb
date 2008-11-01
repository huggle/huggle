Imports System
Imports System.Xml
Imports System.Drawing

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLCenterElement.
    ''' </summary>
    Public Class HTMLCenterElement
        Inherits HTMLElement
        Public Sub New(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
            Initialize(document, parent, xnode)
            Me.Alignment = HTMLAlignment.Center
            ParseChildren(xnode)
        End Sub
        
        Public Overloads Overrides Sub Layout(renderer As HTMLRenderer, g As Graphics)
            'If the current band has items,
            'then start a new band.
            If renderer.CurrentBand().Width > 0 Then
                renderer.NewBand()
            End If
            
            MyBase.Layout(renderer, g)
            renderer.NewBand()
        End Sub
    End Class
End Namespace
