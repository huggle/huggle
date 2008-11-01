Imports System

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLRenderBookmark.
    ''' </summary>
    Public Class HTMLRenderBookmark
        Inherits HTMLRenderItem
        Public Sub New(element As HTMLElement)
            Me.Element = element
        End Sub
        Public Overloads Overrides Sub Paint(g As System.Drawing.Graphics)
            'do nothing, I'm a bookmark
        End Sub
        
    End Class
End Namespace
