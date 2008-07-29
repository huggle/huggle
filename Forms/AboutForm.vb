Class AboutForm

    Private Sub AboutForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Version.Text = "Version " & Config.Version.Major & "." & Config.Version.Minor & "." & Config.Version.Build

        For Each Item As LinkLabel In ContributorsLayoutPanel.Controls
            AddHandler (CType(Item, LinkLabel).LinkClicked), AddressOf ContributorLinkClicked
        Next Item
    End Sub

    Private Sub AboutForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Disclaimer_LinkClicked() Handles Disclaimer.LinkClicked
        Process.Start(Config.DocsLocation)
    End Sub

    Private Sub Icons_LinkClicked() Handles Icons.LinkClicked
        Process.Start(Config.IconsLocation)
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Close()
    End Sub

    Private Sub ContributorLinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
        Process.Start(Config.CreditUrl.Replace("$1", CStr(CType(sender, LinkLabel).Tag)))
    End Sub

End Class