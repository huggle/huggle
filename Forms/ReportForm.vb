Class ReportForm

    Public User As User, Edit As Edit

    Private TrrRequest As Request
    Private BaseEdit As Edit, Reverts As New List(Of Edit)

    Private Sub UserReportForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = "Report " & User.Name
        Message.Focus()

        If Config.AIV Then Reason.Items.Add("Vandalism after final warning")
        If Config.UAA Then Reason.Items.Add("Inappropriate username")
        If Config.TRR Then Reason.Items.Add("Three-revert rule violation")

        Reason.SelectedIndex = 0
        WarnLog.Columns.Add("", 300)
        WarnLog.Items.Add("Retrieving warnings, please wait...")

        Dim NewWarnLogRequest As New WarningLogRequest
        NewWarnLogRequest.Target = WarnLog
        NewWarnLogRequest.User = User
        NewWarnLogRequest.Start()
    End Sub

    Private Sub UserReportForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub UserReportForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click

        Select Case Reason.Text
            Case "Vandalism after final warning"
                Dim NewRequest As New AIVReportRequest
                NewRequest.User = User
                NewRequest.Edit = Edit
                NewRequest.Reason = Message.Text
                NewRequest.Start()

            Case "Inappropriate username"
                Dim NewRequest As New UAAReportRequest
                NewRequest.User = User
                NewRequest.Reason = Message.Text
                NewRequest.Start()

            Case "Three-revert rule violation"
                Dim NewRequest As New TrrReportRequest
                NewRequest.User = User
                If Message.Text <> "" Then NewRequest.Message = Message.Text
                NewRequest.Page = BaseEdit.Page
                NewRequest.BaseEdit = BaseEdit
                NewRequest.Reverts = Reverts
                NewRequest.Start()
        End Select

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Add_Click() Handles Add.Click
        If CurrentEdit IsNot Nothing Then
            If Reverts.Contains(CurrentEdit) Then
                MessageBox.Show("Current revision has already been added to this list.", _
                    "Huggle", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Reverts.Add(CurrentEdit)
                RevertsList.Items.Add(WikiTimestamp(CurrentEdit.Time))
            End If
        End If
    End Sub

    Private Sub AddBase_Click() Handles AddBase.Click
        If CurrentEdit IsNot Nothing AndAlso Not Reverts.Contains(CurrentEdit) Then
            BaseEdit = CurrentEdit
            Base.Text = WikiTimestamp(CurrentEdit.Time)
        End If
    End Sub

    Private Sub Message_TextChanged() Handles Message.TextChanged
        OK.Enabled = (Message.Text <> "")
    End Sub

    Private Sub Reason_SelectedIndexChanged() Handles Reason.SelectedIndexChanged
        Select Case Reason.Text
            Case "Vandalism after final warning"
                If Message.Text = "" OrElse Message.Text = "inappropriate username" Then Message.Text = "vandalism"
                Height = 311

            Case "Inappropriate username"
                If Message.Text = "" OrElse Message.Text = "vandalism" Then Message.Text = "inappropriate username"
                Height = 311

            Case "Three-revert rule violation"
                Message.Text = ""
                Height = 433
        End Select

        TrrPanel.Visible = (Reason.Text = "Three-revert rule violation")
        WarningsPanel.Visible = (Reason.Text <> "Three-revert rule violation")
    End Sub

    Private Sub RevertsList_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles RevertsList.KeyDown
        If e.KeyCode = Keys.Delete AndAlso RevertsList.SelectedIndex > -1 Then
            For Each Item As Edit In Reverts
                If WikiTimestamp(Item.Time) = RevertsList.SelectedItem.ToString Then
                    Reverts.Remove(Item)
                    Exit Sub
                End If
            Next Item

            RevertsList.Items.Remove(RevertsList.SelectedItem)
        End If
    End Sub

    Private Sub RevertsList_SelectedIndexChanged() Handles RevertsList.SelectedIndexChanged
        If RevertsList.SelectedIndex > -1 Then DisplayEdit(Reverts(RevertsList.SelectedIndex))
    End Sub

    Private Sub TrrSearch_Click() Handles TrrSearch.Click
        If TrrSearch.Text = "Cancel" Then
            TrrSearch.Text = "Search"
            Status.Text = "Cancelled."
            TrrRequest.Cancel()
        Else
            Throbber.Start()
            RevertsList.Items.Clear()
            OK.Enabled = False
            TrrSearch.Text = "Cancel"
            Status.Text = "Searching for three-revert rule violations by this user..."
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
                TrrRequest = NewRequest
                NewRequest.User = User
                NewRequest.Start(AddressOf Find3rr)
            End If
        End If
    End Sub

    Private Sub Find3rr(ByVal Result As Request.Output)
        Dim NewRequest As New ThreeRevertRuleCheckRequest
        TrrRequest = NewRequest
        NewRequest.User = User
        NewRequest.Start(AddressOf Find3rrDone)
    End Sub

    Private Sub Find3rrDone(ByVal Result As Request.Output, ByVal BaseEdit As Edit, ByVal Reverts As List(Of Edit))
        Me.BaseEdit = BaseEdit
        Me.Reverts = Reverts
        TrrSearch.Text = "Search"
        Throbber.Stop()

        If Not Result.Success Then
            Status.Text = "Error while searching for three-revert rule violations."
        ElseIf BaseEdit Is Nothing Then
            Status.Text = "No three-revert rule violations found."
        Else
            Status.Text = "Found the following possible three-revert rule violation:"
            ReportWarning.Visible = True
            ReportWarning2.Visible = True
            ReportWarning3.Visible = True

            RevertsList.Items.Add("Revision reverted to: " & WikiTimestamp(BaseEdit.Time))

            For i As Integer = 0 To Reverts.Count - 1
                RevertsList.Items.Add(Ordinal(i + 1) & " revert: " & WikiTimestamp(Reverts(i).Time))
            Next i

            OK.Enabled = True
        End If
    End Sub

End Class