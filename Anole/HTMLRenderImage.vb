Namespace Anole

    Public Class HTMLRenderImage : Inherits HTMLRenderItem

        Public Sub New(ByVal element As HTMLElement)
            Me.Element = element
            Me.Width = element.Width
            Me.Height = element.Height
        End Sub

        Public Overloads Overrides Sub Paint(ByVal g As Graphics)
            Dim ie As HTMLImageElement = TryCast(Me.Element, HTMLImageElement)
            If ie.Bitmap Is Nothing Then Return
            g.DrawImage(ie.Bitmap, Me.Left, Me.Top)
        End Sub

    End Class

End Namespace
