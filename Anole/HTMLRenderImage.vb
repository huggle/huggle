Imports System
Imports System.Drawing
Imports System.Windows.Forms

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLRenderImage.
    ''' </summary>
    Public Class HTMLRenderImage
        Inherits HTMLRenderItem
        Public Sub New(element As HTMLElement)
            Me.Element = element
            Me.Width = element.Width
            Me.Height = element.Height
        End Sub
        
        Public Overloads Overrides Sub Paint(g As Graphics)
            Dim ie As HTMLImageElement = TryCast(Me.Element, HTMLImageElement)
            If ie.Bitmap Is Nothing Then
                Return
            End If
            g.DrawImage(ie.Bitmap, Me.Left, Me.Top)
        End Sub
    End Class
End Namespace
