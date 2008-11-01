Imports System
Imports System.Xml
Imports System.Collections
Imports System.Drawing
Imports System.Text

Namespace Anole
    ''' <summary>
    ''' Base class for all HTML Elements.
    ''' </summary>
    Public Class HTMLElement
        'Properties
        
        Protected myHTMLDocument As HTMLDocument
        Public Property HTMLDocument() As HTMLDocument
            Get
                Return myHTMLDocument
            End Get
            Set
                myHTMLDocument = value
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
        
        Protected myForeColor As Color
        Public Property ForeColor() As Color
            Get
                Return myForeColor
            End Get
            Set
                myForeColor = value
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
        
        Protected myFont As Font
        Public Property Font() As Font
            Get
                Return myFont
            End Get
            Set
                myFont = value
            End Set
        End Property
        
        Protected myParent As HTMLElement
        Public Property Parent() As HTMLElement
            Get
                Return myParent
            End Get
            Set
                myParent = value
            End Set
        End Property
        
        Protected myChildren As ArrayList
        Public Property Children() As ArrayList
            Get
                Return myChildren
            End Get
            Set
                myChildren = value
            End Set
        End Property
        
        Protected myType As String
        Public Property Type() As String
            Get
                Return myType
            End Get
            Set
                myType = value.ToUpper()
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
        
        Protected myNode As XmlNode
        Public Property XmlNode() As XmlNode
            Get
                Return myNode
            End Get
            Set
                myNode = value
            End Set
        End Property
        
        Protected myParser As HTMLParser
        Public Property Parser() As HTMLParser
            Get
                Return myParser
            End Get
            Set
                myParser = value
            End Set
        End Property
        
        Protected myAlignment As HTMLAlignment
        Public Property Alignment() As HTMLAlignment
            Get
                Return myAlignment
            End Get
            Set
                myAlignment = value
            End Set
        End Property
        
        Protected myVAlignment As HTMLVAlignment
        Public Property VAlignment() As HTMLVAlignment
            Get
                Return myVAlignment
            End Get
            Set
                myVAlignment = value
            End Set
        End Property
        
        Protected myName As String
        Public Property Name() As String
            Get
                Return myName
            End Get
            Set
                myName = value
            End Set
        End Property
        
        Protected Sub Initialize(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
            Me.myHTMLDocument = document
            Me.XmlNode = xnode
            Me.Children = New ArrayList()
            Me.Type = xnode.Name.ToUpper()
            Me.myParent = parent
            
            If myParent IsNot Nothing Then
                Me.Parent = parent
                Me.Font = parent.Font
                Me.Alignment = parent.Alignment
                Me.VAlignment = parent.VAlignment
                Me.BackColor = parent.BackColor
                Me.ForeColor = parent.ForeColor
            Else
                'If no parent, start with default settings
                Me.Font = New Font("Arial", 10, FontStyle.Regular)
                Me.Alignment = HTMLAlignment.Left
                Me.VAlignment = HTMLVAlignment.Bottom
                Me.BackColor = Color.White
                Me.ForeColor = Color.Black
            End If
            
            Me.Text = ""
            
            'Parse common attributes
            ParseCommonAttributes(xnode)
        End Sub
        
        Public Sub New()
        End Sub
        
        Public Sub New(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
            Initialize(document, parent, xnode)
            
            If xnode.HasChildNodes Then
                ParseChildren(xnode)
            Else
                Me.Text = xnode.InnerText.Trim()
            End If
        End Sub
        
        ''' <summary>
        ''' Layout is called by HTMLRenderer to instruct HTMLElements to
        ''' create Render items in bands owned by the calling renderer.
        ''' Different HTML elements (i.e. Table cells, images etc.) should
        ''' override this function as needed for their specific layout requiremtns.
        ''' This base class does text layout only.
        ''' </summary>
        ''' <param name="renderer"></param>
        ''' <param name="g"></param>
        Public Overridable Sub Layout(renderer As HTMLRenderer, g As Graphics)
            If Me.Text.Length <> 0 Then
                Dim i As Integer
                'loop counter
                'Split text into substrings delimited by spaces
                'and line feeds
                Dim sSubStrings As String()
                sSubStrings = Me.Text.Split(New Char() {" "c, LF})
                
                
                'Measure the width of each substring based on the current font
                Dim sf As SizeF
                Dim nSubStringWidths As Integer() = New Integer(sSubStrings.Length - 1) {}
                For i = 0 To sSubStrings.Length - 1
                    sSubStrings(i) = sSubStrings(i).Trim()
                    sf = g.MeasureString(sSubStrings(i), Me.Font)
                    nSubStringWidths(i) = CInt(sf.Width)
                Next
                
                'Compute height of current font
                sf = g.MeasureString("W", Me.Font)
                Dim nFontHeight As Integer = CInt(sf.Height)
                
                'Compute the width of a space in the current font
                sf = g.MeasureString(" ", Me.Font)
                Dim nSpaceWidth As Integer = CInt(sf.Width)
                
                Dim band As HTMLRenderBand = renderer.CurrentBand()
                Dim sb As New StringBuilder("")
                Dim sbwidth As Integer = 0
                For i = 0 To sSubStrings.Length - 1
                    'Build string and comput width
                    sb.Append(sSubStrings(i))
                    sbwidth += nSubStringWidths(i)
                    
                    'If this is the last substring just go ahead and add it
                    'to the current band
                    If i = sSubStrings.Length - 1 Then
                        If (band.WidthRemaining() < sbwidth) AndAlso (band.Width <> 0) Then
                            band = renderer.NewBand()
                        End If
                        band.AddItem(New HTMLRenderItem(sb.ToString(), sbwidth + nSpaceWidth, nFontHeight, Me))
                        Exit For
                    End If
                    
                    'If the current band will hold the current string
                    'but not the current string + space + next substring
                    'then add the current string and start a new band
                    If (band.WidthRemaining() >= sbwidth) AndAlso (band.WidthRemaining() < sbwidth + nSpaceWidth + nSubStringWidths(i + 1)) Then
                        band.AddItem(New HTMLRenderItem(sb.ToString(), sbwidth, nFontHeight, Me))
                        band = renderer.NewBand()
                        sb = New StringBuilder()
                        sbwidth = 0
                    'else if the current band is too short to hold the current
                    'create a new band and add it
                    ElseIf band.WidthRemaining() < sbwidth Then
                        band = renderer.NewBand()
                        band.AddItem(New HTMLRenderItem(sb.ToString(), sbwidth, nFontHeight, Me))
                        sb = New StringBuilder()
                        sbwidth = 0
                    Else
                        'add a space and the space's with
                        sb.Append(" ")
                        sbwidth += nSpaceWidth
                        
                    End If
                Next
            End If
            
            'Layout the children
            For Each childelement As HTMLElement In Children
                childelement.Layout(renderer, g)
            Next
        End Sub
        
        Protected Sub ParseChildren(xnode As XmlNode)
            If Not xnode.HasChildNodes Then
                Return
            End If
            For Each childnode As XmlNode In xnode.ChildNodes
                Select Case childnode.Name.ToUpper()
                    Case "BODY"
                        Children.Add(New HTMLBodyElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "FONT"
                        Children.Add(New HTMLFontElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "B"
                        Children.Add(New HTMLBoldElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "U"
                        Children.Add(New HTMLUnderscoreElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "I"
                        Children.Add(New HTMLItalicElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "BR"
                        Children.Add(New HTMLBRElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "HR"
                        Children.Add(New HTMLHRElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "A"
                        Children.Add(New HTMLAElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "CENTER"
                        Children.Add(New HTMLCenterElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "IMG"
                        Children.Add(New HTMLImageElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "TABLE"
                        Children.Add(New HTMLTableElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "TR"
                        Children.Add(New HTMLTRElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case "TD"
                        Children.Add(New HTMLTDElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                    Case Else
                        Children.Add(New HTMLElement(Me.myHTMLDocument, Me, childnode))
                        Exit Select
                End Select
            Next
        End Sub
        
        Protected Sub ParseCommonAttributes(xnode As XmlNode)
            ParseName(xnode)
        End Sub
        
        Protected Sub ParseName(xnode As XmlNode)
            Me.Name = ParseString(xnode, "name", "")
            If Me.Parent Is Nothing Then
                Me.myHTMLDocument.Name = Me.Name
            End If
        End Sub
        
        Protected Sub ParseBackColor(xnode As XmlNode)
            Me.BackColor = HTMLColorParser.ParseColor(Me.ParseString(xnode, "bgcolor", "white"))
        End Sub
        
        Protected Function ParseColor(xnode As XmlNode, defaultcolor As Color) As Color
            Return ParseColor(xnode, "color", defaultcolor)
        End Function
        
        Protected Function ParseColor(xnode As XmlNode, attrname As String, defaultcolor As Color) As Color
            Dim sColorString As String = Me.ParseString(xnode, attrname, "")
            If sColorString.Length = 0 Then
                Return defaultcolor
            End If
            Return HTMLColorParser.ParseColor(sColorString)
        End Function
        
        Protected Function ParseAlignment(xnode As XmlNode, defaultalignment As HTMLAlignment) As HTMLAlignment
            Dim sAlign As String = Me.ParseString(xnode, "align", "left")
            Select Case sAlign.ToUpper()
                Case "LEFT"
                    Return HTMLAlignment.Left
                Case "CENTER"
                    Return HTMLAlignment.Center
                Case "RIGHT"
                    Return HTMLAlignment.Right
                Case "JUSTIFY"
                    Return HTMLAlignment.Justify
                Case Else
                    Return defaultalignment
            End Select
        End Function
        
        
        Protected Sub ParseForeColor(xnode As XmlNode)
            Me.ForeColor = HTMLColorParser.ParseColor(Me.ParseString(xnode, "color", "black"))
        End Sub
        
        
        Protected Function ParseString(xnode As XmlNode, aname As String, sdefault As String) As String
            Try
                Dim attr As XmlNode = xnode.Attributes.GetNamedItem(aname)
                If attr IsNot Nothing Then
                    Return attr.Value.ToString()
                End If
                Return sdefault
            Catch generatedExceptionName As Exception
                Return sdefault
            End Try
            
        End Function
        
        Protected Function ParseInt(xnode As XmlNode, aname As String, ndefault As Integer) As Integer
            Dim attr As XmlNode = xnode.Attributes.GetNamedItem(aname)
            If attr IsNot Nothing Then
                Return Int32.Parse(attr.Value.ToString())
            End If
            Return ndefault
        End Function
        
        Protected Function ParseFloat(xnode As XmlNode, aname As String, ndefault As Single) As Single
            Dim attr As XmlNode = xnode.Attributes.GetNamedItem(aname)
            If attr IsNot Nothing Then
                Return CSng([Double].Parse(attr.Value.ToString()))
            End If
            Return ndefault
        End Function
        
        Public Overloads Overrides Function ToString() As String
            Dim s As String
            s = "<" + Me.Type + " fontsize=" + Me.Font.Size.ToString() + ">"
            s += Me.Text
            For Each childelement As HTMLElement In Children
                s += childelement.ToString()
            Next
            s += "</" + Me.Type + ">"
            Return s
        End Function
        
        
        
    End Class
    
    
    
    Public Enum HTMLAlignment
        Center
        Justify
        Left
        Right
    End Enum
    
    Public Enum HTMLVAlignment
        Top
        Middle
        Bottom
    End Enum
End Namespace
