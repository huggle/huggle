Namespace Anole

    Public Class HTMLRenderItem

        Protected myTop As Integer

        Public Property Top() As Integer
            Get
                Return myTop
            End Get
            Set(ByVal value As Integer)
                myTop = Value
            End Set
        End Property

        Protected myLeft As Integer

        Public Property Left() As Integer
            Get
                Return myLeft
            End Get
            Set(ByVal value As Integer)
                myLeft = Value
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

        Protected myBand As HTMLRenderBand

        Public Property RenderBand() As HTMLRenderBand
            Get
                Return myBand
            End Get
            Set(ByVal value As HTMLRenderBand)
                myBand = Value
            End Set
        End Property

        Protected myHTMLElement As HTMLElement

        Public Property Element() As HTMLElement
            Get
                Return myHTMLElement
            End Get
            Set(ByVal value As HTMLElement)
                myHTMLElement = Value
            End Set
        End Property

        Protected myText As String

        Public Property Text() As String
            Get
                Return myText
            End Get
            Set(ByVal value As String)
                myText = Value
            End Set
        End Property

        Public Sub New()
        End Sub

        Public Sub New(ByVal text As String, ByVal width As Integer, ByVal height As Integer, ByVal element As HTMLElement)
            myText = text
            myWidth = width
            myHeight = height
            myHTMLElement = element
        End Sub

        Public Overridable Sub Paint(ByVal g As Graphics)
            'Pen p=new Pen(Color.Blue);
            'g.DrawRectangle(p,this.Left,myBand.Top+myBand.Height-this.Height,this.Width,this.Height);
            g.DrawString(Me.Text, Me.myHTMLElement.Font, New SolidBrush(Me.myHTMLElement.ForeColor), _
                Me.Left, myBand.Top + myBand.Height - Me.Height)
        End Sub

    End Class

End Namespace
