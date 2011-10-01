Class ProtectionForm

    Public Type As ProtectionType, Page As Page

    Private Sub ReqProtectionForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("protect-request-title", Page.Name)
        Localize(Me, "protect")

        Reason.Text = Config.ProtectionRequestReason
        TypeSelect.SelectedIndex = 0
        ProtectionLog.Page = Page
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

    Private Sub ProtectionLog_EnabledChanged() Handles ProtectionLog.EnabledChanged
        CurrentLevel.Text = Msg("protect-currentlevel") & " "

        If Page.EditLevel = "" AndAlso Page.MoveLevel = "" Then CurrentLevel.Text &= Msg("protect-none")
        If Page.EditLevel <> "" Then CurrentLevel.Text &= "edit:" & Page.EditLevel & " "
        If Page.MoveLevel <> "" Then CurrentLevel.Text &= "move:" & Page.MoveLevel
    End Sub

End Class