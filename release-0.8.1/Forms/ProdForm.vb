Class ProdForm

    Public Page As Page

    Private Sub ProdForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = "Proposed deletion of " & Page.Name
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Reason.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ProdTagForm_FormClosing() Handles Me.FormClosing
        If DialogResult = DialogResult.OK Then
            Dim NewRequest As New TagRequest

            NewRequest.Page = CurrentPage
            NewRequest.Tag = "{{subst:prod|" & Reason.Text & "}}"
            NewRequest.Summary = Config.ProdSummary.Replace("$1", Reason.Text)
            NewRequest.AvoidText = "{{dated prod|"

            If CurrentPage.FirstEdit IsNot Nothing AndAlso CurrentPage.FirstEdit.User IsNot User.Me Then
                NewRequest.NotifyRequest = New UserMessageRequest
                NewRequest.NotifyRequest.Message = Config.ProdMessage.Replace("$1", CurrentPage.Name) _
                    .Replace("$2", Reason.Text)
                NewRequest.NotifyRequest.Title = Config.ProdMessageTitle.Replace("$1", CurrentPage.Name)
                NewRequest.NotifyRequest.AvoidText = CurrentPage.Name
                NewRequest.NotifyRequest.Summary = Config.ProdMessageSummary.Replace("$1", CurrentPage.Name)
                NewRequest.NotifyRequest.User = CurrentPage.FirstEdit.User
            End If

            NewRequest.Start()
        Else
            DialogResult = DialogResult.Cancel
        End If
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

    Private Sub Reason_TextChanged() Handles Reason.TextChanged
        OK.Enabled = (Reason.Text <> "")
    End Sub

End Class