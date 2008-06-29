Class MoveForm

    Public ThisPage As Page

    Private Sub MoveForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Text = "Move " & ThisPage.Name
        Target.Text = ThisPage.Name
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click(ByVal s As Object, ByVal e As EventArgs) Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub MovePageForm_FormClosing(ByVal s As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Reason_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Reason.KeyDown
        If e.KeyCode = Keys.Enter AndAlso OK.Enabled Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub MovePageForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Target_TextChanged(ByVal s As Object, ByVal e As EventArgs) Handles Target.TextChanged
        OK.Enabled = (Target.Text <> ThisPage.Name AndAlso Target.Text <> "" AndAlso Reason.Text <> "")
    End Sub

    Private Sub Reason_TextChanged(ByVal s As Object, ByVal e As EventArgs) Handles Reason.TextChanged
        OK.Enabled = (Target.Text <> ThisPage.Name AndAlso Target.Text <> "" AndAlso Reason.Text <> "")
    End Sub

End Class