Imports System.Text.RegularExpressions
Imports System.Web.HttpUtility

Class BlockForm

    Public User As User

    Private Sub BlockForm_Load() Handles Me.Load
        Icon = My.Resources.icon_red_button
        Text = Msg("block-title", User.Name)
        Localize(Me, "block")

        If Config.BlockMessageDefault Then Message.SelectedIndex = 1 Else Message.SelectedIndex = 0

        Duration.Items.AddRange(Config.BlockExpiryOptions.ToArray)

        If User.Anonymous AndAlso User.SharedIP Then
            SharedIPWarning.Text = Msg("block-sharedipwarning", User.Name)
            SharedIPWarning.Visible = True
            Reason.SelectedIndex = 2
        Else
            SharedIPWarning.Visible = False
        End If

        'Check sensitive IP addresses list
        If User.Anonymous Then
            For Each Item As String In Config.SensitiveAddresses
                If New Regex(Item.Substring(0, Item.IndexOf(";"))).IsMatch(User.Name) Then
                    If MessageBox.Show(Msg("block-sensitivewarning", Item.Substring(Item.IndexOf(";") + 1)), _
                        "Huggle", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) _
                        = DialogResult.No Then

                        DialogResult = DialogResult.Cancel
                        Close()
                    End If

                    Exit For
                End If
            Next Item
        End If

        BlockLog.User = User
        WarnLog.User = User
    End Sub

    Private Sub BlockForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub OK_Click() Handles OK.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub BlockForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub UserTalk_Click() Handles UserTalk.Click
        OpenUrlInBrowser(Config.SitePath & "w/index.php?title=" & urlencode(User.TalkPage.Name))
    End Sub

    Private Sub UserContribs_Click() Handles UserContribs.Click
        OpenUrlInBrowser(Config.SitePath & "w/index.php?title=Special:Contributions/" & User.Name)
    End Sub

    Private Sub Reason_SelectedIndexChanged() Handles Reason.SelectedIndexChanged
        If Reason.Text.StartsWith("{{") Then Message.Text = Reason.Text
    End Sub

End Class