Imports System.Diagnostics

Module Tools

    Public Sub OpenUrlInBrowser(ByVal url As String)
        Try
            Process.Start(url)
        Catch
        End Try
    End Sub

End Module
