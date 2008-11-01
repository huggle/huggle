Imports System
Imports System.Xml
Imports System.Windows.Forms
Imports System.Drawing
Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLBodyElement.
    ''' </summary>
    Public Class HTMLBodyElement
        Inherits HTMLElement
        Public Sub New(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
            Initialize(document, parent, xnode)
            ParseBackColor(xnode)
            document.BackColor = Me.BackColor
            Me.ForeColor = Me.ParseColor(xnode, "text", Color.Black)
            ParseChildren(xnode)
        End Sub
    End Class
End Namespace
