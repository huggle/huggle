Class SpeedyForm

    Public Page As Page

    Private Sub SpeedyForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = "Speedy tag " & Page.Name

        For Each Item As SpeedyCriterion In SpeedyCriteria.Values
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
        Param.Visible = False
        ParamLabel.Visible = False
        Me.Height -= 30

    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Criterion.SelectedIndex > -1 Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub SpeedyTagForm_FormClosing() Handles Me.FormClosing
        If Me.DialogResult = DialogResult.OK Then

            Dim NewSpeedyRequest As New SpeedyRequest
            NewSpeedyRequest.Page = Page
            NewSpeedyRequest.Criterion = SpeedyCriteria(Criterion.Text.Substring(0, Criterion.Text.IndexOf(" ")))
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
        If Criterion.SelectedIndex > -1 _
            Then NotifyCreator.Checked = SpeedyCriteria(Criterion.Text.Substring(0, Criterion.Text.IndexOf(" "))).Notify
    End Sub

    Private Sub SpeedyTagForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Criterion_SelectedIndexChanged() _
        Handles Criterion.SelectedIndexChanged

        If Criterion.SelectedIndex > -1 _
            Then NotifyCreator.Checked = SpeedyCriteria(Criterion.Text.Substring(0, Criterion.Text.IndexOf(" "))).Notify
        OK.Enabled = (Criterion.SelectedIndex <> -1)

        If Criterion.Text.Substring(0, Criterion.Text.IndexOf(" ")) = "G12" Then
            Param.Visible = True
            ParamLabel.Visible = True
            Me.Height += 30
            Param.Text = "url="
        Else
            If Param.Visible = True Then
                Me.Height -= 30
                Param.Visible = False
                ParamLabel.Visible = False
            End If
            End If
    End Sub

End Class
