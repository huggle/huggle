Class TagForm

    Public ThisPage As Page

    Private Sub TagForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Text = "Tag " & ThisPage.Name

        ToSpeedy.Visible = Config.Speedy
        ToProd.Visible = Config.Prod

        TagSelector.Items.AddRange(Config.Tags.ToArray)
        TagText.Text = LastTagText
        TagText.Focus()
        TagText.SelectAll()
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        If TagText.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Cancel_Click(ByVal s As Object, ByVal e As EventArgs) Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub TagForm_FormClosing(ByVal s As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel

        If DialogResult = DialogResult.OK Then
            LastTagText = TagText.Text
            If Summary.Text = "" Then Summary.Text = TagText.Text.Replace(vbCrLf, " ")

            Dim NewRequest As New TagRequest

            NewRequest.ThisPage = CurrentEdit.Page
            NewRequest.Summary = Summary.Text
            NewRequest.Tag = TagText.Text
            NewRequest.InsertAtEnd = InsertAtEnd.Checked
            NewRequest.Start()
        End If
    End Sub

    Private Sub Summary_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Summary.KeyDown
        If e.KeyCode = Keys.Enter AndAlso TagText.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub TagForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub TagText_TextChanged(ByVal s As Object, ByVal e As EventArgs) Handles TagText.TextChanged
        OK.Enabled = (TagText.Text <> "")
        Summary.Text = TagText.Text.Replace(vbCrLf, " ")
        If Summary.Text.Length > 250 Then Summary.Text = Summary.Text.Substring(0, 250)
    End Sub

    Private Sub TagSelector_SelectedIndexChanged(ByVal s As Object, ByVal e As EventArgs) _
        Handles TagSelector.SelectedIndexChanged

        If Not TagText.Text.Contains(CStr(TagSelector.Items(TagSelector.SelectedIndex))) Then
            If TagText.Text <> "" Then TagText.Text &= vbCrLf
            TagText.Text &= CStr(TagSelector.Items(TagSelector.SelectedIndex))
        End If
    End Sub

    Private Sub Explanation_LinkClicked(ByVal s As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles Explanation.LinkClicked

        Dim NewEditForm As New EditForm
        NewEditForm.Page = ThisPage
        NewEditForm.Show()

        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ToSpeedy_Click(ByVal s As Object, ByVal e As EventArgs) Handles ToSpeedy.Click
        DialogResult = DialogResult.Cancel
        Close()
        Main.TagSpeedy_Click(Nothing, Nothing)
    End Sub

    Private Sub ToProd_Click(ByVal s As Object, ByVal e As EventArgs) Handles ToProd.Click
        DialogResult = DialogResult.Cancel
        Close()
        Main.PageTagProd_Click(Nothing, Nothing)
    End Sub

End Class