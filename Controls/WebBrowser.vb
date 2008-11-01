Imports System.IO

Class WebBrowser

    Inherits System.Windows.Forms.WebBrowser

    Public Overloads Property DocumentText() As String
        Get
            Return MyBase.DocumentText
        End Get
        Set(ByVal value As String)
            If Mono() Then SetBrowserTextTheHardWay(value) Else MyBase.DocumentText = value
        End Set
    End Property

    'Mono does not implement the DocumentText property
    'So we save the text we want to display to a file and have the browser load that file instead
    Private Sub SetBrowserTextTheHardWay(ByVal Text As String)
        Dim TempFile As String = Path.GetTempFileName & ".html"
        File.WriteAllText(TempFile, Text)
        Navigate("file:///" & TempFile)
    End Sub

End Class
