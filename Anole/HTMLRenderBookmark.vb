Namespace Anole

    Public Class HTMLRenderBookmark : Inherits HTMLRenderItem

        Public Sub New(ByVal element As HTMLElement)
            Me.Element = element
        End Sub

        Public Overloads Overrides Sub Paint(ByVal g As System.Drawing.Graphics)
            'do nothing, I'm a bookmark
        End Sub

    End Class

End Namespace
