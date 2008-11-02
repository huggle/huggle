Class DeleteForm

    Public Page As Page

    Private Sub DeleteForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("delete-title", Page.Name)
        Localize(Me, "delete")

        If Config.Speedy Then
            For Each Item As SpeedyCriterion In Config.SpeedyCriteria.Values
                If Item.Code = "G8" AndAlso Page.IsTalkPage _
                    OrElse (Item.Code <> "G8" AndAlso Item.Code.StartsWith("G")) _
                    OrElse (Item.Code.StartsWith("A") AndAlso Page.Space.Name = "") _
                    OrElse (Item.Code.StartsWith("C") AndAlso Page.Space.Name = "Category") _
                    OrElse (Item.Code.StartsWith("I") AndAlso Page.Space.Name = "Image") _
                    OrElse (Item.Code.StartsWith("P") AndAlso Page.Space.Name = "Portal") _
                    OrElse (Item.Code.StartsWith("T") AndAlso Page.Space.Name = "Template") _
                    OrElse (Item.Code.StartsWith("U") AndAlso Page.Space.Name.StartsWith("User")) _
                    Then Reason.Items.Add(Item.Code & " - " & Item.Description)
            Next Item
        End If

        DeletionLog.Columns.Add("", 300)
        DeletionLog.Items.Add("Retrieving deletion log, please wait...")

        Dim NewRequest As New DeleteLogRequest
        NewRequest.Target = DeletionLog
        NewRequest.Page = Page
        NewRequest.Start()
    End Sub

    Private Sub DeleteForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub OK_Click() Handles OK.Click
        DialogResult = DialogResult.OK

        Dim Summary As String

        If Reason.SelectedIndex > -1 Then
            Dim Criterion As SpeedyCriterion = Config.SpeedyCriteria(Reason.Text.Substring(0, 3).Trim(" "c).ToUpper)
            Summary = Config.SpeedyDeleteSummary.Replace("$1", _
                "[[WP:SD#" & Criterion.DisplayCode & "|" & Config.SpeedyCriteria(Criterion.DisplayCode).Description & "]]")
        Else
            Summary = Reason.Text
        End If

        Dim NewDeleteRequest As New DeleteRequest
        NewDeleteRequest.Page = Page
        NewDeleteRequest.Summary = Summary
        NewDeleteRequest.Start()

        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
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