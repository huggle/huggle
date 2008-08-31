Class RevertForm

    Public Page As Page, User As User
    Private Shared LastSummary As String

    Private Sub RevertForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = "Revert " & Page.Name
        Summary.Items.AddRange(ManualRevertSummaries.ToArray)
        Summary.Text = LastSummary
        Summary.Focus()
        Summary.SelectAll()
    End Sub

    Private Sub RevertForm_FormClosing() Handles Me.FormClosing
        If DialogResult = DialogResult.OK Then
            If Not ManualRevertSummaries.Contains(Summary.Text) Then ManualRevertSummaries.Add(Summary.Text)
            LastSummary = Summary.Text

            If User.Level = UserLevel.None Then User.Level = UserLevel.Reverted
            If Page.Level = PageLevel.None Then Page.Level = PageLevel.Watch

            Dim NewRevertRequest As New RevertRequest
            NewRevertRequest.Summary = Summary.Text
            NewRevertRequest.Edit = CurrentEdit.Prev
            NewRevertRequest.Start()
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Summary.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub DiffRevertSummaryBForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub Summary_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Summary.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Summary.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Summary_TextChanged() Handles Summary.TextChanged
        OK.Enabled = (Summary.Text <> "")
    End Sub

End Class