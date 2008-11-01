Imports System
Imports System.Drawing

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLRenderTableItem.
    ''' </summary>
    Public Class HTMLRenderTableItem
        Inherits HTMLRenderItem
        Protected myRowCount As Integer
        Protected myColCount As Integer
        Protected myColWidths As Integer()
        Protected myRowHeights As Integer()
        
        Private myTableElement As HTMLTableElement
        
        Public Sub New(rowcount As Integer, colcount As Integer, width As Integer, height As Integer, element As HTMLTableElement, colwidths As Integer(), _
        	rowheights As Integer())
            Me.myHTMLElement = element
            Me.myTableElement = element
            Me.myRowCount = rowcount
            Me.myColCount = colcount
            Me.myWidth = width
            Me.myHeight = height
            Me.myColWidths = colwidths
            Me.myRowHeights = rowheights
        End Sub
        
        Public Overloads Overrides Sub Paint(g As Graphics)
            Dim p As New Pen(Color.Red)
            g.DrawRectangle(p, Left, Top, Width, Height)
            
            Dim l As Integer = Me.Left
            Dim t As Integer = Me.Top
            
            For i As Integer = 0 To myRowCount - 1
                For j As Integer = 0 To myColCount - 1
                    g.DrawRectangle(p, l, t, Me.myColWidths(j), Me.myRowHeights(i))
                    l += Me.myColWidths(j)
                Next
                l = Me.Left
                    
                t += Me.myRowHeights(i)
            Next
            
        End Sub
        
        'g.DrawLine(p,this.Left,this.Top,this.Width,this.Top);
    End Class
End Namespace
