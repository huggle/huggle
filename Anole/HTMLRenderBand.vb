Imports System
Imports System.Collections
Imports System.Drawing

Namespace Anole

    Public Class HTMLRenderBand

        Protected myRenderItems As ArrayList
        Protected myTargetWidth As Integer

        Public Property TargetWidth() As Integer
            Get
                Return myTargetWidth
            End Get
            Set(ByVal value As Integer)
                myTargetWidth = Value
            End Set
        End Property

        Protected myWidth As Integer

        Public Property Width() As Integer
            Get
                Return myWidth
            End Get
            Set(ByVal value As Integer)
                myWidth = Value
            End Set
        End Property

        Protected myHeight As Integer

        Public Property Height() As Integer
            Get
                Return myHeight
            End Get
            Set(ByVal value As Integer)
                myHeight = Value
            End Set
        End Property

        Protected myTop As Integer

        Public Property Top() As Integer
            Get
                Return myTop
            End Get
            Set(ByVal value As Integer)
                myTop = Value
            End Set
        End Property

        Public Sub New(ByVal targetwidth As Integer)
            myTargetWidth = targetwidth
            myRenderItems = New ArrayList()
            myHeight = 0
            myWidth = 0
        End Sub

        Public Function WidthRemaining() As Integer
            If myWidth < myTargetWidth Then
                Return myTargetWidth - myWidth
            End If
            Return 0
        End Function

        Public Function GetItemCount() As Integer
            Return myRenderItems.Count
        End Function

        Public Sub AddItem(ByVal item As HTMLRenderItem)
            myHeight = Math.Max(myHeight, item.Height)
            myWidth += item.Width
            myRenderItems.Add(item)
        End Sub

        ' Called by HTMLRenderer after bands are filled
        ' with RenderItems so alignment can be calculated
        Public Sub ComputeItemCoords()
            Dim left As Integer = 1

            'TBD: valign for table cells

            If myRenderItems.Count = 0 Then Return

            'If an item in the band is centered, then
            'assume all items in that band are centered
            Dim hri As HTMLRenderItem = TryCast(myRenderItems(0), HTMLRenderItem)

            If hri.Element.Alignment = HTMLAlignment.Center Then
                left = (Me.TargetWidth - Me.Width) \ 2
            Else
                left = 1
            End If

            For Each item As HTMLRenderItem In myRenderItems
                item.RenderBand = Me
                item.Left = left
                item.Top = Me.Top
                left += item.Width
            Next

        End Sub

        Public Sub Paint(ByVal g As Graphics)

            '   Pen p=new Pen(Color.Black);
            '	g.DrawRectangle(p,0,this.Top,this.Width,this.Height);
            '	g.FillRectangle(new SolidBrush(Color.White),1,this.Top+1,
            '	this.Width-2,this.Height-2);			

            For Each item As HTMLRenderItem In myRenderItems
                item.Paint(g)
            Next item
        End Sub

        'Returns the item whose bounding rectangle contains the specified point.
        'Used for hyperlink hit-testing.
        Public Function LocateItem(ByVal x As Integer, ByVal y As Integer) As HTMLRenderItem
            For Each item As HTMLRenderItem In Me.myRenderItems
                If (x >= item.Left) AndAlso (x <= item.Left + item.Width) _
                    AndAlso (y >= item.Top) AndAlso (y <= item.Top + item.Height) Then Return item
            Next item

            Return Nothing
        End Function

        Public Function LocateItem(ByVal aname As String) As HTMLRenderItem
            For Each item As HTMLRenderItem In myRenderItems
                If item.Element.Name.Length > 0 AndAlso item.Element.Name.CompareTo(aname) = 0 Then Return item
            Next item

            Return Nothing
        End Function

        Public Overloads Overrides Function ToString() As String
            Dim s As String = ""
            s = "<HTMLRenderBand top:" + Me.Top.ToString() + " width:" + Me.Width.ToString() + _
                " height:" + Me.Height.ToString() + " "

            For Each item As HTMLRenderItem In myRenderItems
                s += item.ToString()
            Next item

            s += " HTMLRenderband>"
            Return s
        End Function

    End Class

End Namespace
