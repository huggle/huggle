Class UserInfoForm

    Public User As User

    Private Sub UserInfoForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = Msg("userinfo-title", User.Name)
        Localize(Me, "userinfo")
        RefreshData()

        BlockLog.User = User
        WarnLog.User = User

        If User.EditCount = -1 Then
            If User.Anonymous Then
                'Edit counts for anonymous users not available through the API, so must use Special:Contributions
                'and if we're going to do that, might as well parse their contributions too,
                'for which we *can* use the API...

                Dim NewContribsRequest As New ContribsRequest
                NewContribsRequest.User = User
                NewContribsRequest.BlockSize = 500
                NewContribsRequest.Start()

            Else
                Dim NewCountRequest As New CountRequest
                NewCountRequest.Users.Add(User)
                NewCountRequest.Start()
            End If
        End If
    End Sub

    Private Sub UserInfoForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click, OK.Click
        Close()
    End Sub

    Public Sub RefreshData()
        SessionEdits.Text = CStr(User.SessionEditCount)
        If User.Anonymous Then Anonymous.Text = Msg("yes") Else Anonymous.Text = Msg("no")
        If User.Ignored Then Ignored.Text = Msg("yes") Else Ignored.Text = Msg("no")
        If User.SharedIP Then SharedIP.Text = Msg("yes") Else SharedIP.Text = Msg("no")
        If User.EditCount > -1 Then Edits.Text = CStr(User.EditCount) Else Edits.Text = "..."
    End Sub

    Public Sub RefreshWarnings()
        WarnLog.Refresh()
    End Sub

    Public Sub RefreshBlocks()
        BlockLog.Refresh()
    End Sub

End Class