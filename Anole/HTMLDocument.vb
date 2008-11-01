Imports System.IO
Imports System.Text

Namespace Anole

    Public Class HTMLDocument
        
        Protected myName As String

        Public Property Name() As String
            Get
                Return myName
            End Get
            Set
                myName = value
            End Set
        End Property
        
        Protected myHTML As String

        Public Property HTML() As String
            Get
                Return myHTML
            End Get
            Set
                myHTML = value
            End Set
        End Property
        
        Protected myBackColor As Color

        Public Property BackColor() As Color
            Get
                Return myBackColor
            End Get
            Set
                myBackColor = value
            End Set
        End Property
        
        Protected myALinkColor As Color

        Public Property ALinkColor() As Color
            Get
                Return myALinkColor
            End Get
            Set
                myALinkColor = value
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
        
        Protected myRootElement As HTMLElement

        Public Property RootElement() As HTMLElement
            Get
                Return myRootElement
            End Get
            Set
                myRootElement = value
            End Set
        End Property
        
        Protected myParser As HTMLParser
        
        Public Sub New()
            myName = ""
            myParser = New HTMLParser()
            Me.BackColor = Color.White
            Me.ALinkColor = Color.Blue
        End Sub
        
        Public Sub LoadHTML(html As String)
            Me.HTML = html
            myParser.Parse(Me)
        End Sub
        
        Public Sub LoadHTMLFile(sFileName As String)
            Dim f As New FileStream(sFileName, FileMode.Open, FileAccess.Read)
            Dim buffer As Byte() = New Byte(2047) {}
            Dim bytesread As Integer = 1
            Dim sb As New StringBuilder

            Do
                bytesread = f.Read(buffer, 0, 2048)
                If bytesread > 0 Then sb.Append(ASCIIEncoding.ASCII.GetString(buffer, 0, bytesread))
            Loop While bytesread <> 0

            f.Close()
            LoadHTML(sb.ToString())
        End Sub
        
    End Class

End Namespace
