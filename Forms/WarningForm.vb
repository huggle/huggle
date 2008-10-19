Class WarningForm

    Public User As User

    Private Sub WarningForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = Msg("warning-title", User.Name)
        Localize(Me, "warning")

        WarnLog.User = User
        WarnType.Items.AddRange(Config.WarningSeries.ToArray)
        If WarnType.Items.Count > 0 Then WarnType.SelectedIndex = 0

        Level2.Visible = (Config.WarningMode <> "1")
        Level3.Visible = (Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2")
        LevelFinal.Visible = (Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2" _
            AndAlso Config.WarningMode <> "3")
    End Sub

    Private Sub WarningForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub WarningForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape : Close_Click()
            Case Keys.Enter : OK_Click()
            Case Keys.D1 : Level1.Checked = True
            Case Keys.D2 : Level2.Checked = True
            Case Keys.D3 : Level3.Checked = True
            Case Keys.D4 : LevelFinal.Checked = True
            Case Keys.A : LevelAuto.Checked = True
        End Select
    End Sub

    Private Sub Close_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click
        DialogResult = DialogResult.OK

        Dim NewWarningRequest As New WarningRequest
        NewWarningRequest.Edit = CurrentEdit

        If LevelAuto.Checked Then NewWarningRequest.Level = 0
        If Level1.Checked Then NewWarningRequest.Level = 1
        If Level2.Checked Then NewWarningRequest.Level = 2
        If Level3.Checked Then NewWarningRequest.Level = 3
        If LevelFinal.Checked Then NewWarningRequest.Level = 4

        NewWarningRequest.Type = WarnType.SelectedItem.ToString
        NewWarningRequest.Start()

        Close()
    End Sub

End Class