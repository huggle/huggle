Class RevertAndWarnForm

    Public User As User

    Private Sub RevertAndWarnForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = Msg("revertandwarn-title", User.Name)

        SummaryLabel.Text = Msg("revert-summary")
        CurrentOnly.Text = Msg("revert-currentonly")
        LevelGroup.Text = Msg("warning-levelgroup")
        LevelAuto.Text = Msg("warning-levelauto")
        Level1.Text = Msg("warning-level1")
        Level2.Text = Msg("warning-level2")
        Level3.Text = Msg("warning-level3")
        LevelFinal.Text = Msg("warning-level4")
        WarnType.Text = Msg("warning-warntype")
        WarnLogLabel.Text = Msg("warning-warnlog")

        Summary.Items.AddRange(Config.RevertSummaries.ToArray)

        WarnLog.Columns.Add("", 300)
        WarnLog.Items.Add("Retrieving warnings, please wait...")
        WarnType.Items.AddRange(Config.WarningSeries.ToArray)
        If WarnType.Items.Count > 0 Then WarnType.SelectedIndex = 0
        WarnType.Focus()

        Level2.Visible = (Config.WarningMode <> "1")
        Level3.Visible = (Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2")
        LevelFinal.Visible = (Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2" _
            AndAlso Config.WarningMode <> "3")

        WarnLog.User = User
    End Sub

    Private Sub RevertAndWarnForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub RevertAndWarnForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape : Cancel_Click()
            Case Keys.Enter : OK_Click()
        End Select
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Not Config.RevertSummaries.Contains(Summary.Text) Then Config.RevertSummaries.Add(Summary.Text)

        Dim Level As UserLevel

        If LevelAuto.Checked Then Level = UserLevel.None
        If Level1.Checked Then Level = UserLevel.Warn1
        If Level2.Checked Then Level = UserLevel.Warn2
        If Level3.Checked Then Level = UserLevel.Warn3
        If LevelFinal.Checked Then Level = UserLevel.WarnFinal

        MainForm.RevertAndWarn(WarnType.Text, Level)
        DialogResult = DialogResult.OK
        Close()
    End Sub

End Class