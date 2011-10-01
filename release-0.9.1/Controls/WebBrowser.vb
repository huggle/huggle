Imports System.IO
Imports System.Text.Encoding

Class WebBrowser

    Inherits System.Windows.Forms.WebBrowser

    Private Shared TempFiles As New Dictionary(Of String, String)
    Private Shared MD5 As System.Security.Cryptography.MD5 = MD5.Create

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

        Dim Hash As String = MD5.ComputeHash(UTF8.GetBytes(Text)).ToString

        If Not TempFiles.ContainsKey(Hash) Then
            Dim TempFileName As String = Path.GetTempFileName & ".html"
            File.WriteAllText(TempFileName, Text)
            TempFiles.Add(Hash, TempFileName)
        End If

        Navigate("file:///" & TempFiles(Hash))
    End Sub

    Public Shared Sub ClearTempFiles()
        For Each Item As String In TempFiles.Values
            If File.Exists(Item) Then File.Delete(Item)
            If File.Exists(Path.GetFileNameWithoutExtension(Item)) _
                Then File.Delete(Path.GetFileNameWithoutExtension(Item))
        Next Item
    End Sub

End Class
