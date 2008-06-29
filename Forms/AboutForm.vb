Class AboutForm

    Private Sub AboutForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Version.Text = "Version " & Config.Version.Major & "." & Config.Version.Minor & "." & Config.Version.MinorRevision
    End Sub

    Private Sub AboutForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        Close()
    End Sub

    Private Sub Credit_LinkClicked(ByVal s As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles Credit.LinkClicked

        Process.Start(CreditUrl)
    End Sub

    Private Sub Disclaimer_LinkClicked(ByVal s As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles Disclaimer.LinkClicked

        Process.Start(Config.DocsLocation)
    End Sub

    Private Sub Icons_LinkClicked(ByVal s As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles Icons.LinkClicked

        Process.Start(Config.IconsLocation)
    End Sub

End Class