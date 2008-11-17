Class ProtectForm

    Public Page As Page

    Private Sub ProtectForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("protect-title", Page.Name)
        Localize(Me, "protect")
        Reason.Text = Config.ProtectionReason
        Expiry.Text = Config.ProtectionTime
        ProtectionLog.Page = Page
    End Sub

    Private Sub ProtectForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Dim NewRequest As New ProtectRequest
        NewRequest.Page = Page
        NewRequest.Summary = Reason.Text
        If SemiProtection.Checked Then NewRequest.EditLevel = "autoconfirmed" _
            Else If FullProtection.Checked Then NewRequest.EditLevel = "sysop"
        If MoveProtection.Checked Then NewRequest.MoveLevel = "sysop"
        If NewRequest.EditLevel <> "sysop" AndAlso NewRequest.MoveLevel <> "sysop" _
            Then NewRequest.MoveLevel = NewRequest.EditLevel
        NewRequest.Expiry = Expiry.Text
        NewRequest.Start()

        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        Close()
    End Sub

    Private Sub Reason_TextChanged() Handles Reason.TextChanged
        OK.Enabled = (Reason.Text <> "")
    End Sub

    Private Sub ProtectionLog_EnabledChanged() Handles ProtectionLog.EnabledChanged
        CurrentLevel.Text = Msg("protect-currentlevel") & " "

        If Page.EditLevel = "" AndAlso Page.MoveLevel = "" Then CurrentLevel.Text &= Msg("protect-noprotection")
        If Page.EditLevel <> "" Then CurrentLevel.Text &= "edit:" & Page.EditLevel & " "
        If Page.MoveLevel <> "" Then CurrentLevel.Text &= "move:" & Page.MoveLevel
    End Sub

    Private Sub SemiProtection_CheckedChanged() Handles SemiProtection.CheckedChanged
        If SemiProtection.Checked Then MoveProtection.Checked = False
    End Sub

    Private Sub FullProtection_CheckedChanged() Handles FullProtection.CheckedChanged
        If FullProtection.Checked Then MoveProtection.Checked = True
    End Sub

End Class