Class WarnLog

    'Control for dislaying user's warning log

    Inherits ListView

    Private _User As User

    Sub New()
        DoubleBuffered = True
        View = Windows.Forms.View.Details
        GridLines = True
        HeaderStyle = ColumnHeaderStyle.Nonclickable
    End Sub

    Public Property User() As User
        Get
            Return _User
        End Get
        Set(ByVal value As User)
            _User = value
            RefreshLog()
        End Set
    End Property

    Public Sub RefreshLog()
        Clear()

        If User IsNot Nothing Then
            Columns.Add("", Width - 10)
            Items.Add(Msg("warnlog-progress"))

            Dim NewRequest As New WarningLogRequest
            NewRequest.User = User
            NewRequest.Start(AddressOf RequestDone)
        End If
    End Sub

    Private Sub RequestDone(ByVal Result As RequestResult)
        If Result.Error Then
            Clear()
            Columns.Add("", Width - 10)
            Items.Add(Result.ErrorMessage)

        ElseIf User.Warnings Is Nothing OrElse User.Warnings.Count = 0 Then
            Clear()
            Columns.Add("", Width - 10)
            Items.Add(Msg("warnlog-none", User.Name))

        Else
            For Each Item As Warning In User.Warnings
                If Item.Time.AddHours(Config.WarningAge) > Date.UtcNow Then
                    If Item.Level > User.Level Then User.Level = Item.Level
                    If User.WarnTime < Item.Time Then User.WarnTime = Item.Time
                End If
            Next Item

            Items.Clear()
            Columns.Clear()
            Columns.Add("Date", 100)
            Columns.Add("Level", 60)
            Columns.Add("Type", 80)
            Columns.Add("User", 120)

            For Each Warning As Warning In User.Warnings
                Dim NewItem As New ListViewItem

                NewItem.Text = Warning.Time.ToShortDateString & " " & Warning.Time.ToShortTimeString
                If Warning.Time.AddHours(36) > Date.UtcNow Then NewItem.ForeColor = Color.Blue

                Select Case Warning.Level
                    Case UserLevel.Notification : NewItem.SubItems.Add("--")
                    Case UserLevel.Warn1 : NewItem.SubItems.Add("Level 1")
                    Case UserLevel.Warn2 : NewItem.SubItems.Add("Level 2")
                    Case UserLevel.Warn3 : NewItem.SubItems.Add("Level 3")
                    Case UserLevel.Warn4im : NewItem.SubItems.Add("Level 4im")
                    Case UserLevel.WarnFinal : NewItem.SubItems.Add("Level 4")
                    Case UserLevel.Blocked : NewItem.SubItems.Add("Blocked")
                    Case Else : NewItem.SubItems.Add("--")
                End Select

                NewItem.SubItems.Add(Warning.Type)

                If Warning.User IsNot Nothing Then NewItem.SubItems.Add(Warning.User.Name) _
                    Else NewItem.SubItems.Add("?")

                Items.Add(NewItem)
            Next Warning
        End If
    End Sub

End Class
