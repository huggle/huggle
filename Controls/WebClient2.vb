Imports System.Net

<DebuggerStepThrough()> _
Class WebClient2

    Inherits WebClient

    'Extend WebClient so we can attach a CookieContainer to each request
    'so cookies sent with redirection responses don't get lost

    Private Shared _Cookies As New CookieContainer

    Public ReadOnly Property Cookies() As CookieContainer
        Get
            Return _Cookies
        End Get
    End Property

    Protected Overrides Function GetWebRequest(ByVal Address As Uri) As WebRequest
        Dim Request As HttpWebRequest = CType(MyBase.GetWebRequest(Address), HttpWebRequest)
        Request.CookieContainer = _Cookies
        Return Request
    End Function

End Class