Class AboutForm

    Private Sub AboutForm_Load() Handles Me.Load
        'Load the icon
        Icon = My.Resources.huggle_icon
        'Get the current version number from config
        Text = "Huggle " & VersionString(Config.Version)
        'Localize the form
        Localize(Me, "about")

        'For each of the old contributers
        For Each Item As LinkLabel In OldContributors.Controls
            'Add a handler for their link click
            AddHandler CType(Item, LinkLabel).LinkClicked, AddressOf ContributorLinkClicked
        Next Item

        'For each of the new contributers
        For Each Item As LinkLabel In NewContributors.Controls
            'Add a handler for their link click
            AddHandler CType(Item, LinkLabel).LinkClicked, AddressOf ContributorLinkClicked
        Next Item
    End Sub

    Private Sub AboutForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        'If the key pressed is the excape key then close
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub ContributorLinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
        'Open the URL for the selected contributer in a browser
        OpenUrlInBrowser(CStr(CType(sender, LinkLabel).Tag))
    End Sub

    Private Sub Disclaimer_LinkClicked() Handles Disclaimer.LinkClicked
        'Open the URL for the project on WP in a browser
        OpenUrlInBrowser(Config.DocsLocation)
    End Sub

    Private Sub Icons_LinkClicked() Handles Icons.LinkClicked
        'Open the URL for the icons in a browser
        OpenUrlInBrowser(Config.IconsLocation)
    End Sub

    Private Sub OK_Click() Handles OK.Click
        'Close
        Close()
    End Sub
End Class