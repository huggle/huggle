Class DeleteForm

    Public ThisPage As Page

    Private Sub DeleteForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load

        If Config.Speedy Then
            For Each Item As SpeedyCriterion In SpeedyCriteria.Values
                If Item.Code = "G8" AndAlso ThisPage.Namespace.ToLower.EndsWith("talk") _
                    OrElse (Item.Code <> "G8" AndAlso Item.Code.StartsWith("G")) _
                    OrElse (Item.Code.StartsWith("A") AndAlso ThisPage.Namespace = "") _
                    OrElse (Item.Code.StartsWith("C") AndAlso ThisPage.Namespace = "Category") _
                    OrElse (Item.Code.StartsWith("I") AndAlso ThisPage.Namespace = "Image") _
                    OrElse (Item.Code.StartsWith("P") AndAlso ThisPage.Namespace = "Portal") _
                    OrElse (Item.Code.StartsWith("T") AndAlso ThisPage.Namespace = "Template") _
                    OrElse (Item.Code.StartsWith("U") AndAlso ThisPage.Namespace.StartsWith("User")) _
                    Then Reason.Items.Add(Item.Code & " - " & Item.Description)
            Next Item
        End If

        Text = "Delete " & ThisPage.Name
        DeleteLog.Columns.Add("", 300)
        DeleteLog.Items.Add("Retrieving deletion log, please wait...")

        Dim NewRequest As New DeleteLogRequest
        NewRequest.Target = DeleteLog
        NewRequest.ThisPage = ThisPage
        NewRequest.Start()
    End Sub

    Private Sub DeleteForm_FormClosing(ByVal s As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        DialogResult = DialogResult.OK

        Dim Summary As String

        If Reason.SelectedIndex > -1 Then
            Dim Criterion As SpeedyCriterion = SpeedyCriteria(Reason.Text.Substring(0, 3).Trim(" "c).ToUpper)
            Summary = Config.SpeedyDeleteSummary.Replace("$1", _
                "[[WP:SD#" & Criterion.DisplayCode & "|" & SpeedyCriteria(Criterion.DisplayCode).Description & "]]")
        Else
            Summary = Reason.Text
        End If

        Dim NewDeleteRequest As New DeleteRequest
        NewDeleteRequest.ThisPage = ThisPage
        NewDeleteRequest.Summary = Summary
        NewDeleteRequest.Start()

        Close()
    End Sub

    Private Sub Cancel_Click(ByVal s As Object, ByVal e As EventArgs) Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub DeleteForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub Reason_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Reason.KeyDown
        If e.KeyCode = Keys.Enter Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

End Class