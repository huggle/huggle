Imports System.Text.RegularExpressions

Class QueueFiltersForm

    Public Queue As EditQueue

    Private Sub QueueFiltersForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button

        ArticlesOnly.Checked = Queue.ArticlesOnly
        If Queue.TitleRegex IsNot Nothing Then TitleRegex.Text = Queue.TitleRegex.ToString

        NamespaceTransformSelector.SelectedIndex = 0
    End Sub

    Private Sub QueueFiltersForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub QueueFiltersForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub NamespaceTransform_Click() Handles NamespaceTransform.Click
        Dim Changes, i As Integer

        While i < Queue.Pages.Count
            Dim Page As Page = GetPage(Queue.Pages(i))

            If NamespaceTransformSelector.SelectedIndex = 0 _
                Then ChangeTitle(Not Page.Namespace.ToLower.Contains("talk"), TalkPageName(Queue.Pages(i)), i, Changes) _
                Else ChangeTitle(Page.Namespace.ToLower.Contains("talk"), SubjectPageName(Queue.Pages(i)), i, Changes)
        End While

        MsgBox(CStr(Changes) & " titles changed.", MsgBoxStyle.Information, "huggle")
    End Sub

    Private Sub ChangeTitle(ByVal Change As Boolean, ByVal NewTitle As String, _
        ByRef i As Integer, ByRef Changes As Integer)

        'Helper subroutine for above
        If Change Then
            Queue.Pages.RemoveAt(i)
            If Not Queue.Pages.Contains(NewTitle) Then Queue.Pages.Add(NewTitle)
            Changes += 1
        Else
            i += 1
        End If
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Queue.ArticlesOnly = ArticlesOnly.Checked

        Try
            If TitleRegex.Text = "" Then Queue.TitleRegex = Nothing _
                Else Queue.TitleRegex = New Regex(TitleRegex.Text, RegexOptions.Compiled)
        Catch ex As ArgumentException
            MsgBox("Value entered for title filter is not a valid regular expression.", _
                MsgBoxStyle.Exclamation, "huggle")
            Exit Sub
        End Try

        Dim i As Integer

        While i < Queue.Pages.Count
            If Queue.MatchesFilter(Queue.Pages(i)) Then i += 1 Else Queue.Pages.RemoveAt(i)
        End While

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        Close()
    End Sub

End Class