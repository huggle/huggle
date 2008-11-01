Class ProtectionForm

    Public Type As ProtectionType
    Public ThisPage As Page

    Private Sub ReqProtectionForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = "Request protection of " & ThisPage.Name
        Reason.Text = Config.ProtectionRequestReason
        TypeSelect.SelectedIndex = 0

        ProtectionLog.Columns.Add("", 300)
        ProtectionLog.Items.Add("Retrieving protection log, please wait...")
        CurrentLevel.Text = "Current protection level: Retrieving..."

        Dim NewRequest As New ProtectionLogRequest
        NewRequest.Target = ProtectionLog
        NewRequest.Page = ThisPage
        NewRequest.Start()
    End Sub

    Private Sub ReqProtectionForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub ReqProtectionForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Reason.Text <> "" Then
            Select Case TypeSelect.Text
                Case "Semi-protection" : Type = ProtectionType.Semi
                Case "Full protection" : Type = ProtectionType.Full
                Case "Move protection" : Type = ProtectionType.Move
            End Select

            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Reason_TextChanged() Handles Reason.TextChanged
        OK.Enabled = (Reason.Text <> "")
    End Sub

    Private Sub ProtectionLog_EnabledChanged() _
        Handles ProtectionLog.EnabledChanged

        CurrentLevel.Text = "Current protection level: "

        If ThisPage.EditLevel = "" AndAlso ThisPage.MoveLevel = "" Then CurrentLevel.Text &= "no protection"
        If ThisPage.EditLevel <> "" Then CurrentLevel.Text &= "edit:" & ThisPage.EditLevel & " "
        If ThisPage.MoveLevel <> "" Then CurrentLevel.Text &= "move:" & ThisPage.MoveLevel
    End Sub

End Class