Imports System.Text.RegularExpressions

Class QueueActionsForm

    Public Queue As Queue

    Private Sub QueueFiltersForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        NamespaceTransformSelector.SelectedIndex = 0
    End Sub

    Private Sub QueueFiltersForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub NamespaceTransform_Click() Handles NamespaceTransform.Click
        Dim Changes, i As Integer

        While i < Queue.Pages.Count
            Dim Page As Page = GetPage(Queue.Pages(i))

            If NamespaceTransformSelector.SelectedIndex = 0 _
                Then ChangeTitle(Not Page.IsTalkPage, Page.TalkPageName, i, Changes) _
                Else ChangeTitle(Page.IsTalkPage, Page.SubjectPageName, i, Changes)
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
        Close()
    End Sub

End Class