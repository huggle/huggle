Class RevertForm

    Public Edit As Edit
    Private Shared LastSummary As String

    Private Sub RevertForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = Msg("revert-title", Edit.Page.Name)
        Localize(Me, "revert")

        Summary.Items.AddRange(Config.RevertSummaries.ToArray)
        Summary.Text = LastSummary
        Summary.Focus()
        Summary.SelectAll()
    End Sub

    Private Sub RevertForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub RevertForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub OK_Click() Handles OK.Click
        DialogResult = DialogResult.OK

        If Not Config.RevertSummaries.Contains(Summary.Text) Then Config.RevertSummaries.Add(Summary.Text)
        LastSummary = Summary.Text
        DoRevert(CurrentEdit, Summary.Text, CurrentOnly:=CurrentOnly.Checked)

        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Summary_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Summary.KeyDown
        If e.KeyCode = Keys.Enter Then OK_Click()
    End Sub

End Class