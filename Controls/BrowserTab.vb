Imports System.Web.HttpUtility

Class BrowserTab

    Public History As New List(Of HistoryItem)
    Public HistoryIndex As Integer
    Public Edit As Edit

    Public BackMenu As New ContextMenuStrip
    Public ForwardMenu As New ContextMenuStrip

    Public Shadows Parent As TabPage
    Public ShowNewEdits, ShowNewContribs, Highlight As Boolean
    Public LastBrowserRequest As BrowserRequest
    Public CurrentUrl As String
    Public NewTab As Boolean = True

    Public Sub AddHistoryItem(ByVal HistoryItem As HistoryItem)
        If History.Count > 0 Then BackMenu.Items.Insert(0, GetMenuItem(History(0)))
        If BackMenu.Items.Count > 10 Then BackMenu.Items.RemoveAt(10)

        History.Insert(0, HistoryItem)
        HistoryIndex = 0

        If CurrentTab Is Me Then
            MainForm.BrowserBackB.DropDown = BackMenu
            MainForm.BrowserForwardB.DropDown = ForwardMenu
        End If
    End Sub

    Public Sub HistoryBack()
        If HistoryIndex < History.Count - 1 Then
            HistoryIndex += 1
            BuildHistoryMenus()

            If History(HistoryIndex).Edit IsNot Nothing _
                Then DisplayEdit(History(HistoryIndex).Edit, True, Me) _
                Else DisplayHistoryItem(History(HistoryIndex), Me)
            MainForm.RefreshInterface()
        End If
    End Sub

    Public Sub HistoryForward()
        If HistoryIndex > 0 Then
            HistoryIndex -= 1
            BuildHistoryMenus()

            If History(HistoryIndex).Edit IsNot Nothing _
                Then DisplayEdit(History(HistoryIndex).Edit, True, Me) _
                Else DisplayHistoryItem(History(HistoryIndex), Me)
            MainForm.RefreshInterface()
        End If
    End Sub

    Private Sub MenuItemClicked(ByVal Sender As Object, ByVal e As EventArgs)
        Dim HistoryItem As HistoryItem = CType(CType(Sender, ToolStripMenuItem).Tag, Misc.HistoryItem)
        HistoryIndex = History.IndexOf(HistoryItem)
        BuildHistoryMenus()

        If HistoryItem.Edit IsNot Nothing _
            Then DisplayEdit(HistoryItem.Edit, True, Me) _
            Else DisplayHistoryItem(HistoryItem, Me)
    End Sub

    Private Sub BuildHistoryMenus()
        BackMenu.Items.Clear()
        ForwardMenu.Items.Clear()

        For i As Integer = HistoryIndex + 1 To Math.Min(History.Count - 1, HistoryIndex + 11)
            BackMenu.Items.Add(GetMenuItem(History(i)))
        Next i

        For i As Integer = HistoryIndex - 1 To Math.Max(0, HistoryIndex - 11) Step -1
            ForwardMenu.Items.Add(GetMenuItem(History(i)))
        Next i
    End Sub

    Private Function GetMenuItem(ByVal HistoryItem As HistoryItem) As ToolStripMenuItem
        Dim NewMenuItem As New ToolStripMenuItem
        NewMenuItem.Tag = HistoryItem

        If HistoryItem.Edit IsNot Nothing Then
            NewMenuItem.Text = HistoryItem.Edit.Page.Name & " rev " & HistoryItem.Edit.Id

        ElseIf WikiUrl(HistoryItem.Url) Then
            Dim UrlParts As Dictionary(Of String, String) = ParseUrl(HistoryItem.Url)
            NewMenuItem.Text = "View " & UrlParts("title")
            If UrlParts.ContainsKey("oldid") Then NewMenuItem.Text &= " rev " & UrlParts("oldid")
        Else
            NewMenuItem.Text = HistoryItem.Url
        End If

        AddHandler NewMenuItem.Click, AddressOf MenuItemClicked
        Return NewMenuItem
    End Function

    Private Sub Browser_Navigating(ByVal s As Object, ByVal e As WebBrowserNavigatingEventArgs) _
        Handles Browser.Navigating

        Dim Url As String = e.Url.ToString
        If Url.StartsWith("about:/") Then Url = SitePath() & Url.Substring(7)
        If Url.StartsWith("about:#") Then Url = SitePath() & "wiki/" & Edit.Page.Name & Url.Substring(6)
        If Url = "about:blank" Then Exit Sub
        If Url.StartsWith("file:///") Then Exit Sub
        e.Cancel = True

        If Config.OpenInBrowser Then
            OpenUrlInBrowser(Url)
        Else
            Dim Params As Dictionary(Of String, String) = ParseUrl(Url)

            If Not Params.ContainsKey("title") Then
                OpenUrlInBrowser(Url)

            ElseIf Params("title").StartsWith("Special:Blockip") Then
                Dim UserName As String = Nothing

                If Params("title").Contains("/") _
                    Then UserName = Params("title").Substring(Params("title").IndexOf("/") + 1) _
                    Else If Params.ContainsKey("user") Then UserName = Params("user")
                If UserName IsNot Nothing AndAlso Administrator Then MainForm.BlockUser(GetUser(UserName))

            ElseIf Params("title") = "Special:Contributions" AndAlso Params.ContainsKey("target") Then
                MainForm.SetCurrentUser(GetUser(Params("target")), True)

            ElseIf Params("title").StartsWith("Special:Contributions/") Then
                MainForm.SetCurrentUser(GetUser(Params("title").Substring(22)), True)

            ElseIf Params("title").StartsWith(Space.User.Name & ":") AndAlso Not Params("title").Contains("/") Then
                MainForm.SetCurrentUser(GetUser(Params("title").Substring(5)), True)

            Else
                MainForm.SetCurrentPage(GetPage(Params("title")), True)
            End If
        End If
    End Sub

    Private Sub BrowserTab_Paint() Handles Me.Paint
        If NewTab Then
            NewTab = False
            DisplayEdit(Edit, False, CurrentTab)
        End If
    End Sub

    Private Sub Browser_PreviewKeyDown(ByVal s As Object, ByVal e As PreviewKeyDownEventArgs) _
        Handles Browser.PreviewKeyDown

        MainForm.Main_KeyDown(Me, New KeyEventArgs(e.KeyData))
    End Sub

End Class
