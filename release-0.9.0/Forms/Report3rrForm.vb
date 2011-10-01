Class Report3rrForm

    Public User As User

    Private TrrRequest As Request
    Private BaseEdit As Edit, Reverts As New List(Of Edit)

    Private Sub Report3rrForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = "Report three-revert rule violation by " & User.Name
        Message.Focus()
        WarnLog.User = User
    End Sub

    Private Sub Report3rrForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Report3rrForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Dim NewRequest As New TrrReportRequest
        NewRequest.User = User
        If Message.Text <> "" Then NewRequest.Message = Message.Text
        NewRequest.Page = BaseEdit.Page
        NewRequest.BaseEdit = BaseEdit
        NewRequest.Reverts = Reverts
        NewRequest.Start()

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Add_Click() Handles Add.Click
        If CurrentEdit IsNot Nothing Then
            If Reverts.Contains(CurrentEdit) Then
                MessageBox.Show("Current revision has already been added to this list.", _
                    "Huggle", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf CurrentEdit.User IsNot User Then
                MessageBox.Show("Current revision was not made by the user you are trying to report.", _
                    "Huggle", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Reverts.Add(CurrentEdit)
                RevertsList.Items.Add(WikiTimestamp(CurrentEdit.Time))
                OK.Enabled = (Base.Text.Length > 0 AndAlso Reverts.Count >= 4)
            End If
        End If
    End Sub

    Private Sub AddBase_Click() Handles AddBase.Click
        If CurrentEdit IsNot Nothing AndAlso Not Reverts.Contains(CurrentEdit) Then
            BaseEdit = CurrentEdit
            Base.Text = WikiTimestamp(CurrentEdit.Time)
            OK.Enabled = (Base.Text.Length > 0 AndAlso Reverts.Count >= 4)
        End If
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
            OK.Enabled = (Base.Text.Length > 0 AndAlso Reverts.Count >= 4)
        End If
    End Sub

    Private Sub RevertsList_SelectedIndexChanged() Handles RevertsList.SelectedIndexChanged
        If RevertsList.SelectedIndex > -1 Then DisplayEdit(Reverts(RevertsList.SelectedIndex))
    End Sub

    Private Sub Search_Click() Handles Search.Click
        If Search.Text = "Cancel" Then
            Search.Text = "Search"
            Status.Text = "Cancelled."
            TrrRequest.Cancel()
        Else
            Throbber.Start()
            RevertsList.Items.Clear()
            OK.Enabled = False
            Search.Text = "Cancel"
            Status.Text = "Searching for three-revert rule violations by this user..."
            ReportWarning1.Visible = False
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

    Private Sub Find3rr(ByVal Result As RequestResult)
        Dim NewRequest As New ThreeRevertRuleCheckRequest
        TrrRequest = NewRequest
        NewRequest.User = User
        NewRequest.Start(AddressOf Find3rrDone)
    End Sub

    Private Sub Find3rrDone(ByVal Result As RequestResult)

        Me.BaseEdit = CType(Result, ThreeRevertRuleCheckRequest.ThreeRevertRuleCheckResult).BaseEdit
        Me.Reverts = CType(Result, ThreeRevertRuleCheckRequest.ThreeRevertRuleCheckResult).Reverts

        Search.Text = "Search"
        Throbber.Stop()

        If Result.Error Then
            Status.Text = Result.ErrorMessage
        ElseIf BaseEdit Is Nothing Then
            Status.Text = "No three-revert rule violations found."
        Else
            Status.Text = "Found the following possible three-revert rule violation:"
            ReportWarning1.Visible = True
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