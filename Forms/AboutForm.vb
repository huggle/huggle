Class AboutForm

    Private Sub AboutForm_Load() Handles Me.Load
        Version.Text = "Version " & Config.Version.Major & "." & Config.Version.Minor & "." & Config.Version.MinorRevision
    End Sub

    Private Sub AboutForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Credit_LinkClicked() Handles Credit.LinkClicked
        Process.Start(CreditUrl)
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

End Class