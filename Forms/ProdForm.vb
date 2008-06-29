Class ProdForm

    Public ThisPage As Page

    Private Sub ProdForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Text = "Proposed deletion of " & ThisPage.Name
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        If Reason.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Cancel_Click(ByVal s As Object, ByVal e As EventArgs) Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ProdTagForm_FormClosing(ByVal s As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Reason_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Reason.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Reason.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub ProdTagForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Reason_TextChanged(ByVal s As Object, ByVal e As EventArgs) Handles Reason.TextChanged
        OK.Enabled = (Reason.Text <> "")
    End Sub

End Class