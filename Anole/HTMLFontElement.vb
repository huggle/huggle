Imports System
Imports System.Xml
Imports System.Drawing
Imports System.Windows.Forms

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLFontElement.
    ''' </summary>
    Public Class HTMLFontElement
        Inherits HTMLElement
        Public Sub New(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
            Initialize(document, parent, xnode)
            
            ParseForeColor(xnode)
            
            Dim fname As String = Me.ParseString(xnode, "face", Me.Font.Name)
            Dim fsize As Single = Me.ParseFloat(xnode, "size", Me.Font.Size)
            
            If (fname.CompareTo(Me.Font.Name) <> 0) OrElse (fsize <> Font.Size) Then
                'Create a new font
                Me.Font = New Font(fname, fsize, Me.Font.Style)
            End If
            
            ParseChildren(xnode)
        End Sub
    End Class
    
    Public Class HTMLBoldElement
        Inherits HTMLElement
        Public Sub New(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
            Initialize(document, parent, xnode)
            Dim fname As String = Me.Font.Name
            Dim fsize As Single = Me.Font.Size
            Dim fs As FontStyle = Me.Font.Style
            fs = fs Or FontStyle.Bold
            Me.Font = New Font(fname, fsize, fs)
            ParseChildren(xnode)
        End Sub
    End Class
    
    Public Class HTMLUnderscoreElement
        Inherits HTMLElement
        Public Sub New(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
            Initialize(document, parent, xnode)
            Dim fname As String = Me.Font.Name
            Dim fsize As Single = Me.Font.Size
            Dim fs As FontStyle = Me.Font.Style
            fs = fs Or FontStyle.Underline
            Me.Font = New Font(fname, fsize, fs)
            ParseChildren(xnode)
        End Sub
    End Class
    
    Public Class HTMLItalicElement
        Inherits HTMLElement
        Public Sub New(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
            Initialize(document, parent, xnode)
            Dim fname As String = Me.Font.Name
            Dim fsize As Single = Me.Font.Size
            Dim fs As FontStyle = Me.Font.Style
            fs = fs Or FontStyle.Italic
            Me.Font = New Font(fname, fsize, fs)
            ParseChildren(xnode)
        End Sub
    End Class
End Namespace
