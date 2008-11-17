Class SpeedyForm

    Public Page As Page

    Private Sub SpeedyForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = "Speedy tag " & Page.Name

        For Each Item As SpeedyCriterion In Config.SpeedyCriteria.Values
            If Item.Code = "G8" AndAlso Page.IsTalkPage _
                OrElse (Item.Code <> "G8" AndAlso Item.Code.StartsWith("G")) _
                OrElse (Item.Code.StartsWith("A") AndAlso Page.IsArticle) _
                OrElse (Item.Code.StartsWith("C") AndAlso Page.Space.Name = "Category") _
                OrElse (Item.Code.StartsWith("I") AndAlso Page.Space.Name = "Image") _
                OrElse (Item.Code.StartsWith("P") AndAlso Page.Space.Name = "Portal") _
                OrElse (Item.Code.StartsWith("T") AndAlso Page.Space.Name = "Template") _
                OrElse (Item.Code.StartsWith("U") AndAlso Page.Space.Name.StartsWith("User")) _
                Then Criterion.Items.Add(Item.Code & " - " & Item.Description)
        Next Item

        Height -= 25
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Criterion.SelectedIndex > -1 Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub SpeedyTagForm_FormClosing() Handles Me.FormClosing
        If DialogResult = DialogResult.OK Then

            Dim NewSpeedyRequest As New SpeedyRequest
            NewSpeedyRequest.Page = Page
            NewSpeedyRequest.Criterion = Config.SpeedyCriteria(Criterion.Text.Substring(0, Criterion.Text.IndexOf(" ")))
            NewSpeedyRequest.Notify = NotifyCreator.Checked
            NewSpeedyRequest.Parameter = Param.Text
            NewSpeedyRequest.Start()
        Else
            DialogResult = DialogResult.Cancel
        End If
    End Sub

    Private Sub Criterion_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Criterion.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Criterion.SelectedIndex > -1 Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Criterion_TextChanged() Handles Criterion.TextChanged
        If Criterion.SelectedIndex > -1 Then NotifyCreator.Checked = _
            Config.SpeedyCriteria(Criterion.Text.Substring(0, Criterion.Text.IndexOf(" "))).Notify
    End Sub

    Private Sub SpeedyTagForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Criterion_SelectedIndexChanged() Handles Criterion.SelectedIndexChanged

        If Criterion.SelectedIndex > -1 Then
            NotifyCreator.Checked = Config.SpeedyCriteria(Criterion.Text.Substring(0, Criterion.Text.IndexOf(" "))).Notify
            OK.Enabled = (Criterion.SelectedIndex <> -1)

            If Criterion.Text.Substring(0, Criterion.Text.IndexOf(" ")) = "G12" Then
                'If g12 is selected add the param options and resize the form to allow them to be seen
                Param.Visible = True
                ParamLabel.Visible = True
                Height += 25

                'Param text for g12 should be url= so set it
                Param.Text = "url="

            ElseIf Param.Visible Then
                Height -= 25
                Param.Visible = False
                ParamLabel.Visible = False
            End If
        End If
    End Sub

End Class
