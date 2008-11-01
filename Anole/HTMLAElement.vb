Namespace Anole

    Public Class HTMLAElement : Inherits HTMLElement

        Protected myHREF As String

        Public Property HREF() As String
            Get
                Return myHREF
            End Get
            Set(ByVal value As String)
                myHREF = value
            End Set
        End Property

        Public Sub New(ByRef document As HTMLDocument, ByVal parent As HTMLElement, ByVal xnode As XmlNode)
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

        Public Overloads Overrides Sub Layout(ByVal renderer As HTMLRenderer, ByVal g As Graphics)
            If Me.Name.Length > 0 Then
                renderer.CurrentBand().AddItem(New HTMLRenderBookmark(Me))
            End If
            MyBase.Layout(renderer, g)
        End Sub

    End Class

End Namespace
