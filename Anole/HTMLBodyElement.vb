Namespace Anole

    Public Class HTMLBodyElement : Inherits HTMLElement

        Public Sub New(ByRef document As HTMLDocument, ByVal parent As HTMLElement, ByVal xnode As XmlNode)
            Initialize(document, parent, xnode)
            ParseBackColor(xnode)
            document.BackColor = Me.BackColor
            Me.ForeColor = Me.ParseColor(xnode, "text", Color.Black)
            ParseChildren(xnode)
        End Sub

    End Class

End Namespace
