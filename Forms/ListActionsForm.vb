Imports System.Text.RegularExpressions

Class ListActionsForm

    Public List As List(Of String), Form As ListForm

    Private Sub ListFiltersForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        NamespaceTransformSelector.SelectedIndex = 0
    End Sub

    Private Sub ListFiltersForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub NamespaceTransform_Click() Handles NamespaceTransform.Click
        Dim Changes, i As Integer

        While i < List.Count
            Dim Page As Page = GetPage(List(i))

            If NamespaceTransformSelector.SelectedIndex = 0 _
                Then ChangeTitle(Not Page.IsTalkPage, Page.TalkPageName, i, Changes) _
                Else ChangeTitle(Page.IsTalkPage, Page.SubjectPageName, i, Changes)
        End While

        List.Sort()

        MessageBox.Show(CStr(Changes) & " titles changed.", "Huggle", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ChangeTitle(ByVal Change As Boolean, ByVal NewTitle As String, _
        ByRef i As Integer, ByRef Changes As Integer)

        'Helper subroutine for above
        If Change Then
            List.RemoveAt(i)
            If Not List.Contains(NewTitle) Then List.Add(NewTitle)
            Changes += 1
        Else
            i += 1
        End If
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Form.TitleRegex = New Regex(TitleRegex.Text)
        Form.Spaces.Clear()

        For Each Item As Space In Namespaces.CheckedItems
            Form.Spaces.Add(Item)
        Next Item

        Close()
    End Sub

End Class