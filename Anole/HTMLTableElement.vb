Namespace Anole

    Public Class HTMLTableElement : Inherits HTMLElement

        Protected myTargetWidth As Integer

        Public Property TargetWidth() As Integer
            Get
                Return myTargetWidth
            End Get
            Set(ByVal value As Integer)
                myTargetWidth = value
            End Set
        End Property

        Protected myBorderSize As Integer

        Public Property BorderSize() As Integer
            Get
                Return myBorderSize
            End Get
            Set(ByVal value As Integer)
                myBorderSize = value
            End Set
        End Property

        Protected myCellSpacing As Integer

        Public Property CellSpacing() As Integer
            Get
                Return myCellSpacing
            End Get
            Set(ByVal value As Integer)
                myCellSpacing = value
            End Set
        End Property

        Protected myCellPadding As Integer

        Public Property CellPadding() As Integer
            Get
                Return myCellPadding
            End Get
            Set(ByVal value As Integer)
                myCellPadding = value
            End Set
        End Property

        Public Sub New(ByRef document As HTMLDocument, ByVal parent As HTMLElement, ByVal xnode As XmlNode)
            Initialize(document, parent, xnode)

            'Get backcolor or use document default color
            Me.BackColor = Me.ParseColor(xnode, "bgcolor", document.BackColor)
            Me.myTargetWidth = Me.ParseInt(xnode, "width", 0)
            Me.myBorderSize = Me.ParseInt(xnode, "borders", 1)
            Me.myCellPadding = Me.ParseInt(xnode, "cellpadding", 0)
            Me.myCellSpacing = Me.ParseInt(xnode, "cellspacing", 0)
            ParseChildren(xnode)
        End Sub

        Public Overloads Overrides Sub Layout(ByVal renderer As HTMLRenderer, ByVal g As System.Drawing.Graphics)
            'Determine number of rows and columns
            Dim nRowCount As Integer = 0
            Dim nColCount As Integer = 0

            nRowCount = Me.Children.Count
            Dim firstrow As HTMLTRElement = TryCast(Me.Children(0), HTMLTRElement)

            If nRowCount > 0 Then
                nColCount = firstrow.Children.Count
            Else
                System.Windows.Forms.MessageBox.Show("No rows in table")
                Return
            End If

            'If the table width is not set, assume
            'that the width is the width of the renderer
            If myTargetWidth = 0 Then myTargetWidth = renderer.TargetWidth

            'Determine an initial column count
            Dim nTargetColumnWidth As Integer = myTargetWidth \ nColCount

            'Create an array of column widths and
            'set to the intial column width
            Dim nColumnWidths As Integer() = New Integer(nColCount - 1) {}
            For i As Integer = 0 To nColCount - 1
                nColumnWidths(i) = nTargetColumnWidth
            Next i

            'TBD: loop thru the first tr and see if any
            'td elements override the default column width

            'Create an array to track row-heights
            Dim nRowHeights As Integer() = New Integer(nRowCount - 1) {}

            'Give each row the column widths
            Dim trelement As HTMLTRElement

            For i As Integer = 0 To Me.Children.Count - 1
                trelement = TryCast(Children(i), HTMLTRElement)
                nRowHeights(i) = trelement.ComputeRowDimensions(nColumnWidths, g)
            Next i

            'Compute table height
            Dim tableheight As Integer = 0

            For i As Integer = 0 To nRowHeights.Length - 1
                nRowHeights(i) = 10
                tableheight += nRowHeights(i)
            Next

            'Compute table width
            Dim tablewidth As Integer = 0
            For i As Integer = 0 To nColumnWidths.Length - 1
                tablewidth += nColumnWidths(i)
            Next i

            'Start a new band
            Dim band As HTMLRenderBand = renderer.NewBand()

            band.AddItem(New HTMLRenderTableItem(nRowCount, nColCount, tablewidth, tableheight, Me, nColumnWidths, _
             nRowHeights))

            'Add a final band
            renderer.NewBand()
        End Sub

    End Class

    Public Class HTMLTRElement : Inherits HTMLElement

        Protected myColumnWidths As Integer()

        Public Sub New(ByRef document As HTMLDocument, ByVal parent As HTMLElement, ByVal xnode As XmlNode)
            Initialize(document, parent, xnode)
            Me.BackColor = Me.ParseColor(xnode, "bgcolor", parent.BackColor)
            ParseChildren(xnode)
        End Sub

        ' Given an array of column widths fills in the max of columnwidths
        ' Returns the height of the row
        Public Function ComputeRowDimensions(ByRef nColumnWidths As Integer(), ByVal g As Graphics) As Integer
            Dim r As HTMLRenderer
            Dim td As HTMLTDElement
            Dim rowheight As Integer = 0

            For i As Integer = 0 To Me.Children.Count - 1
                'Create a renderer and have it render our td item and all its children
                td = TryCast(Me.Children(i), HTMLTDElement)
                r = New HTMLRenderer(td, nColumnWidths(i))

                'Store the max of either the computed width or the original width
                nColumnWidths(i) = Math.Max(nColumnWidths(i), r.Width)

                'Store the height of row
                rowheight = Math.Max(rowheight, r.Height)
            Next i

            Return rowheight
        End Function

    End Class

    Public Class HTMLTDElement : Inherits HTMLElement

        Protected myTargetWidth As Integer

        Public Property TargetWidth() As Integer
            Get
                Return myTargetWidth
            End Get
            Set(ByVal value As Integer)
                myTargetWidth = value
            End Set
        End Property

        Public Sub New(ByRef document As HTMLDocument, ByVal parent As HTMLElement, ByVal xnode As XmlNode)
            Initialize(document, parent, xnode)
            Me.BackColor = Me.ParseColor(xnode, "bgcolor", parent.BackColor)
            Me.Alignment = Me.ParseAlignment(xnode, HTMLAlignment.Left)
            Me.myTargetWidth = Me.ParseInt(xnode, "width", 0)

            ParseChildren(xnode)
        End Sub

    End Class

End Namespace
