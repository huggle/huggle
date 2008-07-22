Imports System.Text.RegularExpressions

Class BlockForm

    Public ThisUser As User

    Private Sub BlockForm_Load() Handles Me.Load
        Text = "Block " & ThisUser.Name

        If Config.BlockMessageDefault Then BlockMessage.SelectedIndex = 1 Else BlockMessage.SelectedIndex = 0

        Expiry.Items.AddRange(Config.BlockExpiryOptions.ToArray)

        If ThisUser.Anonymous AndAlso ThisUser.SharedIP Then
            SharedIPWarning.Text = "Note: " & ThisUser.Name & " is tagged as a dynamic or shared IP address."
            SharedIPWarning.Visible = True
            Reason.SelectedIndex = 2
        Else
            SharedIPWarning.Visible = False
        End If

        'Check sensitive IP addresses list
        If ThisUser.Anonymous Then
            For Each Item As String In Config.SensitiveAddresses
                If New Regex(Item.Substring(0, Item.IndexOf(";"))).IsMatch(ThisUser.Name) Then
                    If MsgBox("This IP address is listed as 'sensitive' for the following reason:" & vbCrLf & vbCrLf & _
                        "   " & Item.Substring(Item.IndexOf(";") + 1) & vbCrLf & vbCrLf & "Continue anyway?", _
                        MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton2, _
                        "Block " & ThisUser.Name) = MsgBoxResult.No Then

                        DialogResult = DialogResult.Cancel
                        Close()
                    End If

                    Exit For
                End If
            Next Item
        End If

        BlockLog.Columns.Add("", 300)
        BlockLog.Items.Add("Retrieving block log, please wait...")

        Dim NewBlockLogRequest As New BlockLogRequest
        NewBlockLogRequest.Target = BlockLog
        NewBlockLogRequest.ThisUser = ThisUser
        NewBlockLogRequest.Start()

        WarnLog.Columns.Add("", 300)
        WarnLog.Items.Add("Retrieving warnings, please wait...")

        Dim NewWarnLogRequest As New WarningLogRequest
        NewWarnLogRequest.Target = WarnLog
        NewWarnLogRequest.ThisUser = ThisUser
        NewWarnLogRequest.Start()
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
        Process.Start(SitePath & "w/index.php?title=User_talk:" & ThisUser.Name)
    End Sub

    Private Sub UserContribs_Click() Handles UserContribs.Click
        Process.Start(SitePath & "w/index.php?title=Special:Contributions/" & ThisUser.Name)
    End Sub

    Private Sub Reason_SelectedIndexChanged() _
        Handles Reason.SelectedIndexChanged

        If Reason.Text.StartsWith("{{") Then BlockMessage.Text = Reason.Text
    End Sub

End Class