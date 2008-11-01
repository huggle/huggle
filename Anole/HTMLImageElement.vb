Imports System.IO
Imports System.Reflection

Namespace Anole

    Public Class HTMLImageElement : Inherits HTMLElement

        Protected mySource As String

        Public Property Source() As String
            Get
                Return mySource
            End Get
            Set(ByVal value As String)
                mySource = value
            End Set
        End Property

        Protected myBitmap As Bitmap

        Public Property Bitmap() As Bitmap
            Get
                Return myBitmap
            End Get
            Set(ByVal value As Bitmap)
                myBitmap = value
            End Set
        End Property

        Public Sub New(ByRef document As HTMLDocument, ByVal parent As HTMLElement, ByVal xnode As XmlNode)
            Initialize(document, parent, xnode)

            'Parse the image name
            mySource = ParseString(xnode, "src", "")

            'See if file exists
            If Not File.Exists(mySource) Then
                'See if the file exists in the current directory
                Dim sPath As String = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()(0).FullyQualifiedName) + "\"
                If File.Exists(sPath + mySource) Then mySource = sPath + mySource
            End If

            If Not File.Exists(mySource) Then Return

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

        Public Overloads Overrides Sub Layout(ByVal renderer As HTMLRenderer, ByVal g As Graphics)
            If myBitmap Is Nothing Then Return

            Dim band As HTMLRenderBand = renderer.CurrentBand()

            If band.WidthRemaining() < myBitmap.Width Then band = renderer.NewBand()
            band.AddItem(New HTMLRenderImage(Me))
        End Sub

    End Class

End Namespace
