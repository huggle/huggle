Class StatsForm

    Private Sub StatsForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("stats-title")
        Localize(Me, "stats")
        RefreshStats()
    End Sub

    Private Sub StatsForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub RefreshStats()
        Actions.BeginUpdate()
        Actions.Clear()
        Actions.Columns.Add("", 150)

        For Each Column As String In Stats.Groups.Keys
            Actions.Columns.Add(Msg("stats-" & Column), 70)
        Next Column

        For Each Row As String In Stats.Group.ItemNames
            Dim NewItem As New ListViewItem(Msg("stats-" & Row))

            For Each Column As String In Stats.Groups.Keys
                NewItem.SubItems.Add(Stats.Groups(Column)(Row).ToString)
            Next Column

            Actions.Items.Add(NewItem)
        Next Row

        Actions.EndUpdate()

        Dim SessionTime As TimeSpan = (Date.UtcNow - StartTime)

        Session.Text = Msg("stats-session", SessionTime.Hours & ":" & SessionTime.Minutes.ToString.PadLeft(2, "0"c) _
            & ":" & SessionTime.Seconds.ToString.PadLeft(2, "0"c))
    End Sub

    Private Sub Actions_ColumnWidthChanging(ByVal s As Object, ByVal e As ColumnWidthChangingEventArgs) _
        Handles Actions.ColumnWidthChanging

        e.Cancel = True
    End Sub

    Private Sub CloseButton_Click() Handles OK.Click
        Close()
    End Sub

    Private Sub StatsTimer_Tick() Handles StatsTimer.Tick
        RefreshStats()
    End Sub

End Class