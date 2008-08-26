Class ReportForm

    Public User As User, Edit As Edit

    Private Sub UserReportForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = "Report " & User.Name
        Reason.SelectedIndex = 0
        Message.Focus()

        WarnLog.Columns.Add("", 300)
        WarnLog.Items.Add("Retrieving warnings, please wait...")

        Dim NewWarnLogRequest As New WarningLogRequest
        NewWarnLogRequest.Target = WarnLog
        NewWarnLogRequest.ThisUser = User
        NewWarnLogRequest.Start()
    End Sub

    Private Sub UserReportForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub UserReportForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Reason.SelectedIndex = 0 Then
            Dim NewRequest As New AIVReportRequest
            NewRequest.User = User
            NewRequest.Edit = Edit
            NewRequest.Reason = Message.Text
            NewRequest.Start()

        ElseIf Reason.SelectedIndex = 1 Then
            Dim NewRequest As New UAAReportRequest
            NewRequest.User = User
            NewRequest.Reason = Message.Text
            NewRequest.Start()
        End If

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Message_TextChanged() Handles Message.TextChanged
        OK.Enabled = (Message.Text <> "")
    End Sub

    Private Sub ReportTo_SelectedIndexChanged() Handles Reason.SelectedIndexChanged
        Splitter.Panel2Collapsed = (Reason.SelectedIndex <> 2)
        Splitter.Panel1Collapsed = (Reason.SelectedIndex = 2)
        Reversions.Items.Clear()

        Select Case Reason.SelectedIndex
            Case 0
                If Message.Text = "" OrElse Message.Text = "inappropriate username" Then Message.Text = "vandalism"
            Case 1
                If Message.Text = "" OrElse Message.Text = "vandalism" Then Message.Text = "inappropriate username"
        End Select

        If Reason.SelectedIndex = 2 Then
            Throbber.Start()
            Status.Text = "Searching for three-revert rule violations by this user..."
            Reversions.Visible = False
            ReportWarning.Visible = False
            ReportWarning2.Visible = False
            ReportWarning3.Visible = False

            Dim ThisEdit As Edit = User.LastEdit
            Dim ContribsComplete As Boolean

            While True
                If ThisEdit.PrevByUser Is Nothing Then Exit While

                If ThisEdit.PrevByUser Is NullEdit Then
                    ContribsComplete = True
                    Exit While
                End If

                If ThisEdit.Time.AddHours(24) > Date.UtcNow Then
                    ContribsComplete = True
                    Exit While
                End If

                ThisEdit = ThisEdit.PrevByUser
            End While

            If ContribsComplete Then
                Find3rr(Nothing)
            Else
                Dim NewRequest As New ContribsRequest
                NewRequest.User = User
                NewRequest.Start(AddressOf Find3rr)
            End If
        End If
    End Sub

    Private Sub Reversions_DoubleClick(ByVal s As Object, ByVal e As EventArgs) Handles Reversions.DoubleClick
        If Reversions.SelectedIndex > -1 Then DisplayEdit(Edit.All(Reversions.SelectedItem.ToString.Substring _
            (Reversions.SelectedItem.ToString.LastIndexOf(" ") + 1)))
    End Sub

    Private Sub Find3rr(ByVal Result As Request.Output)
        Dim NewRequest As New ThreeRevertRuleCheckRequest
        NewRequest.User = User
        NewRequest.Start(AddressOf Find3rrDone)
    End Sub

    Private Sub Find3rrDone(ByVal Result As Request.Output, _
        ByVal VersionId As String, ByVal RevertIds As List(Of String))

        Throbber.Stop()

        If Not Result.Success Then
            Status.Text = "Error while searching for three-revert rule violations"
        ElseIf VersionId Is Nothing Then
            Status.Text = "No three-revert rule violations found"
        Else
            Status.Text = "Found the following possible three-revert rule violation:"
            Reversions.Visible = True
            ReportWarning.Visible = True
            ReportWarning2.Visible = True
            ReportWarning3.Visible = True

            Reversions.Items.Add("Revision reverted to: " & VersionId)

            For i As Integer = 0 To RevertIds.Count - 1
                Reversions.Items.Add(Ordinal(i + 1) & " reversion: " & RevertIds(i))
            Next i
        End If
    End Sub

End Class