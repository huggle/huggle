Class WebBrowser

    Inherits System.Windows.Forms.WebBrowser

    Public Overloads Property DocumentText() As String
        Get
            Return MyBase.DocumentText
        End Get
        Set(ByVal value As String)
            If Not Mono() Then MyBase.DocumentText = value
        End Set
    End Property

End Class
