Namespace Anole

    Public Class HTMLRenderHRItem : Inherits HTMLRenderItem

        Public Sub New(ByVal width As Integer, ByVal element As HTMLElement)
            Me.myHTMLElement = element
            Me.Width = width
        End Sub

        Public Overloads Overrides Sub Paint(ByVal g As Graphics)
            Dim p As New Pen(Color.Black)
            g.DrawLine(p, Me.Left, Me.Top, Me.Width, Me.Top)

            'TBD: take into account hr attributes when drawing
        End Sub

    End Class

End Namespace
