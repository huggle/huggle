Imports System
Imports System.Drawing

Namespace Anole

    Public Class HTMLRenderTableItem : Inherits HTMLRenderItem

        Protected myRowCount, myColCount As Integer
        Protected myColWidths, myRowHeights As Integer()

        Private myTableElement As HTMLTableElement

        Public Sub New(ByVal rowcount As Integer, ByVal colcount As Integer, ByVal width As Integer, _
            ByVal height As Integer, ByVal element As HTMLTableElement, ByVal colwidths As Integer(), ByVal rowheights As Integer())

            Me.myHTMLElement = element
            Me.myTableElement = element
            Me.myRowCount = rowcount
            Me.myColCount = colcount
            Me.myWidth = width
            Me.myHeight = height
            Me.myColWidths = colwidths
            Me.myRowHeights = rowheights
        End Sub

        Public Overloads Overrides Sub Paint(ByVal g As Graphics)
            Dim p As New Pen(Color.Red)
            g.DrawRectangle(p, Left, Top, Width, Height)

            Dim l As Integer = Me.Left
            Dim t As Integer = Me.Top

            For i As Integer = 0 To myRowCount - 1
                For j As Integer = 0 To myColCount - 1
                    g.DrawRectangle(p, l, t, Me.myColWidths(j), Me.myRowHeights(i))
                    l += Me.myColWidths(j)
                Next j

                l = Me.Left
                t += Me.myRowHeights(i)
            Next i
        End Sub

        'g.DrawLine(p,this.Left,this.Top,this.Width,this.Top);

    End Class

End Namespace
