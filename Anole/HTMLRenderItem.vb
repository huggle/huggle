Imports System
Imports System.Drawing

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLRenderItem.
    ''' </summary>
    Public Class HTMLRenderItem
        
        Protected myTop As Integer
        Public Property Top() As Integer
            Get
                Return myTop
            End Get
            Set
                myTop = value
            End Set
        End Property
        
        Protected myLeft As Integer
        Public Property Left() As Integer
            Get
                Return myLeft
            End Get
            Set
                myLeft = value
            End Set
        End Property
        
        Protected myWidth As Integer
        Public Property Width() As Integer
            Get
                Return myWidth
            End Get
            Set
                myWidth = value
            End Set
        End Property
        
        Protected myHeight As Integer
        Public Property Height() As Integer
            Get
                Return myHeight
            End Get
            Set
                myHeight = value
            End Set
        End Property
        
        Protected myBand As HTMLRenderBand
        Public Property RenderBand() As HTMLRenderBand
            Get
                Return myBand
            End Get
            Set
                myBand = value
            End Set
        End Property
        
        Protected myHTMLElement As HTMLElement
        Public Property Element() As HTMLElement
            Get
                Return myHTMLElement
            End Get
            Set
                myHTMLElement = value
            End Set
        End Property
        
        Protected myText As String
        Public Property Text() As String
            Get
                Return myText
            End Get
            Set
                myText = value
            End Set
        End Property
        
        Public Sub New()
        End Sub
        
        Public Sub New(text As String, width As Integer, height As Integer, element As HTMLElement)
            myText = text
            myWidth = width
            myHeight = height
            myHTMLElement = element
        End Sub
        
        Public Overridable Sub Paint(g As Graphics)
            'Pen p=new Pen(Color.Blue);
            'g.DrawRectangle(p,this.Left,myBand.Top+myBand.Height-this.Height,this.Width,this.Height);
            g.DrawString(Me.Text, Me.myHTMLElement.Font, New SolidBrush(Me.myHTMLElement.ForeColor), Me.Left, myBand.Top + myBand.Height - Me.Height)
        End Sub
        
    End Class
End Namespace
