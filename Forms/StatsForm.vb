Class StatsForm

    Private Sub StatsForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button

        For i As Integer = 0 To 3
            Actions.Items(i).SubItems.Add("0")
            Actions.Items(i).SubItems.Add("0")
        Next i

        RefreshStats()
    End Sub

    Private Sub RefreshStats()
        Actions.Items(0).SubItems(1).Text = CStr(Stats.Edits)
        Actions.Items(0).SubItems(2).Text = CStr(Stats.EditsMe)
        Actions.Items(1).SubItems(1).Text = CStr(Stats.Reverts)
        Actions.Items(1).SubItems(2).Text = CStr(Stats.RevertsMe)
        Actions.Items(2).SubItems(1).Text = CStr(Stats.Warnings)
        Actions.Items(2).SubItems(2).Text = CStr(Stats.WarningsMe)
        Actions.Items(3).SubItems(1).Text = CStr(Stats.Blocks)
        Actions.Items(3).SubItems(2).Text = CStr(Stats.BlocksMe)
    End Sub

    Private Sub StatsTimer_Tick() Handles StatsTimer.Tick
        RefreshStats()
    End Sub

    Private Sub StatsForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub CloseButton_Click() Handles CloseButton.Click
        Close()
    End Sub

    Private Sub StatsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class