Class UserInfoForm

    Public ThisUser As User

    Private Sub UserInfoForm_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Text = "User:" & ThisUser.Name
        SessionEditCount.Text = CStr(ThisUser.SessionEditCount)

        BlockLog.Columns.Add("", 300)
        BlockLog.Items.Add("Retrieving block log, please wait...")
        RefreshBlocks()

        WarnLog.Columns.Add("", 300)
        WarnLog.Items.Add("Retrieving warnings, please wait...")
        RefreshWarnings()

        If ThisUser.Level = UserL.Ignore Then Whitelisted.Text = "Yes" Else Whitelisted.Text = "No"
        If ThisUser.SharedIP Then SharedIP.Text = "Yes" Else SharedIP.Text = "No"

        If ThisUser.EditCount > -1 Then
            EditCount.Text = CStr(ThisUser.EditCount)

        ElseIf ThisUser.Anonymous Then
            'Edit counts for anonymous users not available through the API, so must use Special:Contributions
            'and if we're going to do that, might as well parse their contributions too

            Dim NewContribsRequest As New ContribsRequest
            NewContribsRequest.ThisUser = ThisUser
            NewContribsRequest.BlockSize = 500
            NewContribsRequest.Start()

        Else
            Dim NewCountRequest As New CountRequest
            NewCountRequest.Users.Add(ThisUser)
            NewCountRequest.Start()
        End If
    End Sub

    Private Sub OK_Click(ByVal s As Object, ByVal e As EventArgs) Handles OK.Click, OK.Click
        Close()
    End Sub

    Public Sub RefreshWarnings()
        Dim NewWarnLogRequest As New WarningLogRequest
        NewWarnLogRequest.Target = WarnLog
        NewWarnLogRequest.ThisUser = ThisUser
        NewWarnLogRequest.Start()
    End Sub

    Public Sub RefreshBlocks()
        Dim NewBlockLogRequest As New BlockLogRequest
        NewBlockLogRequest.Target = BlockLog
        NewBlockLogRequest.ThisUser = ThisUser
        NewBlockLogRequest.Start()
    End Sub

    Private Sub UserInfoForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Collapse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Collapse.Click
        WarnLog.Visible = Not WarnLog.Visible
        BlockLog.Visible = WarnLog.Visible
        WarnLabel.Visible = WarnLog.Visible
        BlockLabel.Visible = WarnLog.Visible

        If WarnLog.Visible Then
            Height = 404
            Collapse.Image = My.Resources.up_gray
        Else
            Height = 120
            Collapse.Image = My.Resources.down_gray
        End If
    End Sub

End Class