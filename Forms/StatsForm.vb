Class StatsForm

    Private Sub StatsForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        RefreshStats()
    End Sub

    Private Sub StatsForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub RefreshStats()
        Actions.BeginUpdate()
        Actions.Clear()
        Actions.Columns.Add("", 100)

        For Each Column As String In Stats.Groups.Keys
            Actions.Columns.Add(Column, 70)
        Next Column

        For Each Row As String In Stats.Group.ItemNames
            Dim NewItem As New ListViewItem(Row)

            For Each Column As String In Stats.Groups.Keys
                NewItem.SubItems.Add(Stats.Groups(Column)(Row).ToString)
            Next Column

            Actions.Items.Add(NewItem)
        Next Row

        Actions.EndUpdate()
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