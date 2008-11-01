Imports System
Imports System.Drawing
Imports System.Xml
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLImageElement.
    ''' </summary>
    Public Class HTMLImageElement
        Inherits HTMLElement
        Protected mySource As String
        Public Property Source() As String
            Get
                Return mySource
            End Get
            Set
                mySource = value
            End Set
        End Property
        
        Protected myBitmap As Bitmap
        Public Property Bitmap() As Bitmap
            Get
                Return myBitmap
            End Get
            Set
                myBitmap = value
            End Set
        End Property
        
        Public Sub New(ByRef document As HTMLDocument, parent As HTMLElement, xnode As XmlNode)
            Initialize(document, parent, xnode)
            
            'Parse the image name
            mySource = ParseString(xnode, "src", "")
            
            'See if file exists
            If Not System.IO.File.Exists(mySource) Then
                'See if the file exists in the current directory
                Dim sPath As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()(0).FullyQualifiedName) + "\"
                If System.IO.File.Exists(sPath + mySource) Then
                    mySource = sPath + mySource
                End If
            End If
            If Not System.IO.File.Exists(mySource) Then
                Return
            End If
            
            'Load the bitmap
            Try
                myBitmap = New Bitmap(mySource)
                Me.Width = myBitmap.Width
                Me.Height = myBitmap.Height
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                myBitmap = Nothing
            End Try
        End Sub
        
        Public Overloads Overrides Sub Layout(renderer As HTMLRenderer, g As Graphics)
            If myBitmap Is Nothing Then
                Return
            End If
            Dim band As HTMLRenderBand = renderer.CurrentBand()
            If band.WidthRemaining() < myBitmap.Width Then
                band = renderer.NewBand()
            End If
            
            band.AddItem(New HTMLRenderImage(Me))
            
        End Sub
    End Class
End Namespace
