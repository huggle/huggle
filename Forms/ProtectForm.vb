'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for protect form
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