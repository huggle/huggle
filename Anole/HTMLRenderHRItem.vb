Imports System
Imports System.Drawing

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLRenderHRItem.
    ''' </summary>
    Public Class HTMLRenderHRItem
        Inherits HTMLRenderItem
        Public Sub New(width As Integer, element As HTMLElement)
            Me.myHTMLElement = element
            Me.Width = width
        End Sub
        
        Public Overloads Overrides Sub Paint(g As Graphics)
            Dim p As New Pen(Color.Black)
            g.DrawLine(p, Me.Left, Me.Top, Me.Width, Me.Top)
            
            'TBD: take into account hr attributes when drawing
        End Sub
    End Class
End Namespace
