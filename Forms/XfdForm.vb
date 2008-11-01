Class XfdForm

    Public Page As Page

    Private Sub XfdForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = "Nominate '" & Page.Name & "' for deletion"
        Category.Visible = Page.IsArticle
        CategoryLabel.Visible = Page.IsArticle
        If Category.Visible Then Category.SelectedIndex = 0

        If Page.Space Is Space.Article Then
            NominationType.Text = "Article"
        ElseIf Page.Space Is Space.Category Then
            NominationType.Text = "Category"
        ElseIf Page.Space Is Space.Image Then
            NominationType.Text = "Image"
        Else
            NominationType.Text = "Miscellaneous"
        End If
    End Sub

    Private Sub XfdForm_FormClosing() Handles MyBase.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub XfdForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub Reason_TextChanged() Handles Reason.TextChanged
        OK.Enabled = (Reason.Text <> "")
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Page.Space Is Space.Article Then
            Dim NewRequest As New AfdRequest
            NewRequest.Category = Category.Text.Substring(0, 1).Replace("(", "?")
            NewRequest.Page = Page
            NewRequest.Reason = Reason.Text
            NewRequest.Notify = Notify.Checked
            NewRequest.Start()

        ElseIf Page.Space Is Space.Category Then
            Dim NewRequest As New CfdRequest
            NewRequest.Page = Page
            NewRequest.Reason = Reason.Text
            NewRequest.Notify = Notify.Checked
            NewRequest.Start()

        ElseIf Page.Space Is Space.Template Then
            Dim NewRequest As New TfdRequest
            NewRequest.Page = Page
            NewRequest.Reason = Reason.Text
            NewRequest.Notify = Notify.Checked
            NewRequest.Start()

        ElseIf Page.Space Is Space.Image Then
            Dim NewRequest As New IfdRequest
            NewRequest.Page = Page
            NewRequest.Reason = Reason.Text
            NewRequest.Notify = Notify.Checked
            NewRequest.Start()

        Else
            Dim NewRequest As New MfdRequest
            NewRequest.Page = Page
            NewRequest.Reason = Reason.Text
            NewRequest.Notify = Notify.Checked
            NewRequest.Start()
        End If

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Notify_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Notify.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Reason.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

End Class