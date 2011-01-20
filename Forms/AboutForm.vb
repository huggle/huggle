Class AboutForm

    Private Sub AboutForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = "Huggle " & VersionString(Config.Version)
        Localize(Me, "about")

        For Each Item As LinkLabel In OldContributors.Controls
            AddHandler CType(Item, LinkLabel).LinkClicked, AddressOf ContributorLinkClicked
        Next Item
    End Sub

    Private Sub AboutForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub ContributorLinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
        OpenUrlInBrowser(CStr(CType(sender, LinkLabel).Tag))
    End Sub

    Private Sub Disclaimer_LinkClicked() Handles Disclaimer.LinkClicked
        OpenUrlInBrowser(Config.DocsLocation)
    End Sub

    Private Sub Icons_LinkClicked() Handles Icons.LinkClicked
        OpenUrlInBrowser(Config.IconsLocation)
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Close()
    End Sub

    Private Sub Contributor8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenUrlInBrowser(CStr("http://en.wikipedia.org/w/index.php?title=User:Petrb"))
    End Sub

End Class