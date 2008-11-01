Imports System
Imports System.Drawing
Imports System.Xml

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLAElement.
    ''' </summary>
    Public Class HTMLAElement
        Inherits HTMLElement
        Protected myHREF As String
        Public Property HREF() As String
            Get
                Return myHREF
            End Get
            Set
                myHREF = value
            End Set
        End Property
        
        Public Sub New(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
            Initialize(document, parent, xnode)
            
            'Set color to link color
            Me.ForeColor = document.ALinkColor
            
            'Get the HREF
            Me.myHREF = Me.ParseString(xnode, "href", "")
            
            'Set font to underscored
            Dim fname As String = Me.Font.Name
            Dim fsize As Single = Me.Font.Size
            Dim fs As FontStyle = Me.Font.Style
            fs = fs Or FontStyle.Underline
            Me.Font = New Font(fname, fsize, fs)
            ParseChildren(xnode)
        End Sub
        
        Public Overloads Overrides Sub Layout(renderer As HTMLRenderer, g As Graphics)
            If Me.Name.Length > 0 Then
                renderer.CurrentBand().AddItem(New HTMLRenderBookmark(Me))
            End If
            MyBase.Layout(renderer, g)
        End Sub
        
    End Class
End Namespace
