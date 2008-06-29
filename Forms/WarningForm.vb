Class WarningForm

    Public ThisUser As User

    Private Sub WarningForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Text = "Warn " & ThisUser.Name

        WarnLog.Columns.Add("", 300)
        WarnLog.Items.Add("Retrieving warnings, please wait...")
        WarnType.Items.AddRange(Config.WarningSeries.ToArray)
        If WarnType.Items.Count > 0 Then WarnType.SelectedIndex = 0
        
        Level2.Visible = (Config.WarningMode <> "1")
        Level3.Visible = (Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2")
        LevelFinal.Visible = (Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2" _
            AndAlso Config.WarningMode <> "3")

        Dim NewWarnLogRequest As New WarningLogRequest
        NewWarnLogRequest.Target = WarnLog
        NewWarnLogRequest.ThisUser = ThisUser
        NewWarnLogRequest.Start()
    End Sub

    Private Sub Close_Click(ByVal s As Object, ByVal e As EventArgs) Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click
        DialogResult = DialogResult.OK

        Dim NewWarningRequest As New WarningRequest
        NewWarningRequest.ThisEdit = CurrentEdit

        If LevelAuto.Checked Then NewWarningRequest.Level = 0
        If Level1.Checked Then NewWarningRequest.Level = 1
        If Level2.Checked Then NewWarningRequest.Level = 2
        If Level3.Checked Then NewWarningRequest.Level = 3
        If LevelFinal.Checked Then NewWarningRequest.Level = 4

        NewWarningRequest.Type = WarnType.SelectedItem.ToString
        NewWarningRequest.Start()

        Close()
    End Sub

    Private Sub WarningForm_FormClosing(ByVal s As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub WarningForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape : Close_Click(Nothing, Nothing)
            Case Keys.Enter : OK_Click(Nothing, Nothing)
            Case Keys.D1 : Level1.Checked = True
            Case Keys.D2 : Level2.Checked = True
            Case Keys.D3 : Level3.Checked = True
            Case Keys.D4 : LevelFinal.Checked = True
            Case Keys.A : LevelAuto.Checked = True
        End Select
    End Sub

End Class