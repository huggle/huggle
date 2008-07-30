Class TagForm

    Public ThisPage As Page

    Private Sub TagForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = "Tag " & ThisPage.Name

        ToSpeedy.Visible = Config.Speedy
        ToProd.Visible = Config.Prod

        TagSelector.Items.AddRange(Config.Tags.ToArray)
        TagText.Text = LastTagText
        TagText.Focus()
        TagText.SelectAll()
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If TagText.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub TagForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel

        If DialogResult = DialogResult.OK Then
            LastTagText = TagText.Text
            If Summary.Text = "" Then Summary.Text = TagText.Text.Replace(vbCrLf, " ")

            Dim NewRequest As New TagRequest

            NewRequest.Page = CurrentEdit.Page
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

    Private Sub TagText_TextChanged() Handles TagText.TextChanged
        OK.Enabled = (TagText.Text <> "")
        Summary.Text = TagText.Text.Replace(vbCrLf, " ")
        If Summary.Text.Length > 250 Then Summary.Text = Summary.Text.Substring(0, 250)
    End Sub

    Private Sub TagSelector_SelectedIndexChanged() _
        Handles TagSelector.SelectedIndexChanged

        If Not TagText.Text.Contains(CStr(TagSelector.Items(TagSelector.SelectedIndex))) Then
            If TagText.Text <> "" Then TagText.Text &= vbCrLf
            TagText.Text &= CStr(TagSelector.Items(TagSelector.SelectedIndex))
        End If
    End Sub

    Private Sub Explanation_LinkClicked() _
        Handles Explanation.LinkClicked

        Dim NewEditForm As New EditForm
        NewEditForm.Page = ThisPage
        NewEditForm.Show()

        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ToSpeedy_Click() Handles ToSpeedy.Click
        DialogResult = DialogResult.Cancel
        Close()
        MainForm.TagSpeedy_Click()
    End Sub

    Private Sub ToProd_Click() Handles ToProd.Click
        DialogResult = DialogResult.Cancel
        Close()
        MainForm.PageTagProd_Click()
    End Sub

End Class