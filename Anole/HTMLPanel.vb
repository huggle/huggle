Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLPanel.
    ''' </summary>
    Public Class HTMLPanel
        Inherits System.Windows.Forms.Panel
        Protected myDocument As HTMLDocument
        Protected myRenderer As HTMLRenderer
        Protected myHScrollBar As HScrollBar
        Protected myVScrollBar As VScrollBar
        Protected bIgnoreScrolling As Boolean
        Protected myBitmap As Bitmap
        Protected myPictureBox As PictureBox
        Protected myButton As Button
        
        
        ''' <summary>
        ''' Delegate subscribers must implement to listen to hyperlink clicks
        ''' </summary>
        ''' 
        Public Delegate Sub LinkClickHandler(HTMLPanel As Object, htmlEventInfo As HTMLEventArgs)
        
        'Publish OnLinkClick event
        Public Event OnLinkClick As LinkClickHandler
        
        
        Public Sub New()
            InitializeComponent()
            myDocument = New HTMLDocument()
            bIgnoreScrolling = False
        End Sub
        
        Public Property HTML() As String
            Get
                Return myDocument.HTML
            End Get
            Set
                Me.LoadHTML(value)
            End Set
        End Property
        
        Private Sub InitializeComponent()
            myHScrollBar = New HScrollBar()
            Me.Controls.Add(myHScrollBar)
            AddHandler myHScrollBar.ValueChanged, AddressOf myHScrollBar_ValueChanged
            
            myVScrollBar = New VScrollBar()
            Me.Controls.Add(myVScrollBar)
            AddHandler myVScrollBar.ValueChanged, AddressOf myVScrollBar_ValueChanged
            
            'Add event handlers
            AddHandler Resize, AddressOf HTMLPanel_Resize
            
            myPictureBox = New PictureBox()
            myPictureBox.Location = New Point(0, 0)
            AddHandler myPictureBox.MouseDown, AddressOf myPictureBox_MouseDown
            Me.Controls.Add(myPictureBox)
            
            SizeScrollBars()
            
        End Sub
        
        Public Sub LoadHTML(sHTML As String)
            myDocument = New HTMLDocument()
            myDocument.LoadHTML(sHTML)
            Render()
        End Sub
        
        Public Sub LoadHTMLFile(sFileName As String)
            myDocument = New HTMLDocument()
            myDocument.LoadHTMLFile(sFileName)
            Render()
        End Sub
        
        Protected Sub Render()
            myRenderer = New HTMLRenderer(myDocument.RootElement, Me.Width - 19)
            
            If (myRenderer.Width = 0) OrElse (myRenderer.Height = 0) Then
                Return
            End If
            
            myBitmap = New Bitmap(myRenderer.Width, myRenderer.Height)
            Dim g As Graphics = Graphics.FromImage(myBitmap)
            g.Clear(myDocument.BackColor)
            myRenderer.Paint(g)
            
            Me.myPictureBox.Width = myBitmap.Width
            Me.myPictureBox.Height = myBitmap.Height
            Me.myPictureBox.Image = myBitmap
            Me.myPictureBox.Top = 0
            Me.myPictureBox.Left = 0
            
            SizeScrollBars()
            
        End Sub
        
        Public Overloads Overrides Function ToString() As String
            Return myDocument.RootElement.ToString()
        End Function
        
        Public Function GetRendererInfo() As String
            Return myRenderer.ToString()
        End Function
        
        Private Sub HTMLPanel_Resize(sender As Object, e As EventArgs)
            SizeScrollBars()
        End Sub
        
        Public Sub SizeScrollBars()
            bIgnoreScrolling = True
            
            If Me.myRenderer Is Nothing Then
                myHScrollBar.Visible = False
                myVScrollBar.Visible = False
            End If
            
            Me.myHScrollBar.Value = 0
            Me.myVScrollBar.Value = 0
            
            If Me.myRenderer IsNot Nothing Then
                
                If myRenderer.Height > Me.Height Then
                    myVScrollBar.Location = New System.Drawing.Point(Me.Width - 16, 0)
                    myVScrollBar.Size = New System.Drawing.Size(16, Me.Height)
                    
                    'if(myHScrollBar.Visible)
                    '	myVScrollBar.Size=new System.Drawing.Size(16,this.Height-16);
                    'else
                    
                    myVScrollBar.Value = 0
                    myVScrollBar.Maximum = myRenderer.Height
                    myVScrollBar.SmallChange = 10
                    myVScrollBar.LargeChange = myRenderer.Height \ 10
                    myVScrollBar.Visible = True
                Else
                    myVScrollBar.Visible = False
                End If
                
                Dim hWidth As Integer = Me.Width
                If myVScrollBar.Visible Then
                    hWidth -= 16
                End If
                
                If myRenderer.Width > hWidth Then
                    myHScrollBar.Location = New System.Drawing.Point(0, Me.Height - 16)
                    If Me.myVScrollBar.Visible Then
                        myHScrollBar.Size = New System.Drawing.Size(Me.Width - 16, 16)
                        myVScrollBar.Height -= 16
                    Else
                        myHScrollBar.Size = New System.Drawing.Size(Me.Width, 16)
                    End If
                    
                    myHScrollBar.Value = 0
                    myHScrollBar.Maximum = myRenderer.Width - Me.Width + 16 + 16 + 16
                    myHScrollBar.SmallChange = 10
                    myHScrollBar.LargeChange = myRenderer.Width \ 10
                        
                    myHScrollBar.Visible = True
                Else
                    myHScrollBar.Visible = False
                    
                End If
            End If
            
            If myHScrollBar.Visible AndAlso myVScrollBar.Visible Then
                myButton.Visible = True
                myButton.Left = Me.Width - 16
                myButton.Top = Me.Height - 16
                myButton.BringToFront()
            End If
            
            bIgnoreScrolling = False
        End Sub
        
        Private Sub myHScrollBar_ValueChanged(sender As Object, e As EventArgs)
            If bIgnoreScrolling Then
                Return
            End If
            Me.myPictureBox.Left = -myHScrollBar.Value
            'this.Invalidate();
        End Sub
        
        Private Sub myVScrollBar_ValueChanged(sender As Object, e As EventArgs)
            If bIgnoreScrolling Then
                Return
            End If
            Me.myPictureBox.Top = -myVScrollBar.Value
            'this.myPictureBox.Update();
            'this.myPictureBox.Image=this.myBitmap;
            'this.Invalidate();
        End Sub
        
        Private Sub myPictureBox_MouseDown(sender As Object, e As MouseEventArgs)
            'Check to see if mouse was pressed down on
            'a hyperlink
            Dim docx As Integer = e.X
            Dim docy As Integer = e.Y
            Dim item As HTMLRenderItem = Me.myRenderer.LocateItem(docx, docy)
            If item IsNot Nothing Then
                Dim element As HTMLElement = item.Element.Parent
                If TypeOf element Is HTMLAElement Then
                    Dim aelement As HTMLAElement = TryCast(element, HTMLAElement)
                    
                    'If anyone has subscribed to
                    'link click events, notify them
                    RaiseEvent OnLinkClick(Me, New HTMLEventArgs(Me.myDocument.Name, aelement.Name, aelement.HREF))
                    
                    'If the href is a bookmark
                    'find it's coordinates and scroll to it
                    If aelement.HREF.Trim().StartsWith("#") Then
                        Dim name As String = aelement.HREF.Substring(1, aelement.HREF.Length - 1)
                        Dim p As Point = Me.myRenderer.LocateItem(name)
                        If p <> Point.Empty Then
                            Me.myVScrollBar.Value = p.Y
                        End If
                        
                    End If
                End If
            End If
            
        End Sub
    End Class
    
    
    ''' <summary>
    ''' Passed to subscribers of OnLinkClick. Details information about the anchor
    ''' that was clicked.
    ''' </summary>
    Public Class HTMLEventArgs
        Inherits EventArgs
        
        Public ReadOnly documentname As String
        Public ReadOnly elementname As String
        Public ReadOnly href As String
        
        Public Sub New(documentname As String, elementname As String, href As String)
            Me.documentname = documentname
            Me.elementname = elementname
            Me.href = href
        End Sub
        
        Public Overloads Overrides Function ToString() As String
            Return Me.documentname + ":" + Me.elementname + ":" + Me.href
        End Function
        
    End Class
End Namespace
