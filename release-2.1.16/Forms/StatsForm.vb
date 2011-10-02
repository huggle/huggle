'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for stats form
'last modified by Petrb

'Copyright (C) 2011 Huggle team

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.


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