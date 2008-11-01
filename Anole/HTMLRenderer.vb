Imports System.Collections

Namespace Anole

    Public Class HTMLRenderer

        Private myRenderBands As ArrayList
        Private myTargetWidth As Integer
        Private myWidth As Integer

        Public Property Width() As Integer
            Get
                Return myWidth
            End Get
            Set(ByVal value As Integer)
                myWidth = value
            End Set
        End Property

        Private myHeight As Integer

        Public Property Height() As Integer
            Get
                Return myHeight
            End Get
            Set(ByVal value As Integer)
                myHeight = value
            End Set
        End Property

        Public Property TargetWidth() As Integer
            Get
                Return myTargetWidth
            End Get
            Set(ByVal value As Integer)
                myTargetWidth = value
            End Set
        End Property

        Public Sub New(ByVal element As HTMLElement, ByVal targetwidth As Integer)
            If element Is Nothing Then Return

            myWidth = 0
            myHeight = 0

            myTargetWidth = targetwidth
            myRenderBands = New ArrayList()
            myRenderBands.Add(New HTMLRenderBand(targetwidth))
            Dim b As New Bitmap(1, 1)
            Dim g As Graphics = Graphics.FromImage(b)
            element.Layout(Me, g)

            'Calculate band tops
            Dim top, width As Integer

            For Each band As HTMLRenderBand In myRenderBands
                band.Top = top
                top += band.Height + 1
                band.ComputeItemCoords()
                width = Math.Max(width, band.Width)
            Next band

            Me.Height = top
            Me.Width = width
        End Sub

        Public Function CurrentBand() As HTMLRenderBand
            Return TryCast(myRenderBands(myRenderBands.Count - 1), HTMLRenderBand)
        End Function

        Public Function NewBand() As HTMLRenderBand
            Dim b As New HTMLRenderBand(myTargetWidth)
            myRenderBands.Add(b)
            Return b
        End Function

        Public Sub Paint(ByVal g As Graphics)
            'MessageBox.Show(myRenderBands.Count.ToString());
            For Each band As HTMLRenderBand In myRenderBands
                band.Paint(g)
            Next
        End Sub

        ' Returns the item whose bounding rectangle contains the specified point.
        ' Used for hyperlink hit-testing.
        Public Function LocateItem(ByVal x As Integer, ByVal y As Integer) As HTMLRenderItem
            For Each band As HTMLRenderBand In Me.myRenderBands
                If y >= band.Top AndAlso y <= band.Top + band.Height Then Return band.LocateItem(x, y)
            Next band

            Return Nothing
        End Function

        ' Returns the x,y coordinate of the upper left
        ' corner of the item with the given name
        Public Function LocateItem(ByVal aname As String) As Point
            Dim item As HTMLRenderItem

            For Each band As HTMLRenderBand In Me.myRenderBands
                item = band.LocateItem(aname)
                If item IsNot Nothing Then Return New Point(item.Left, item.Top)
            Next band

            Return Point.Empty
        End Function

        Public Overloads Overrides Function ToString() As String
            Dim s As String = "<HTMLRenderer "

            For Each band As HTMLRenderBand In myRenderBands
                s += band.ToString()
            Next band

            s += " HTMLRenderer>"
            Return s
        End Function

    End Class

End Namespace

