'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for delete form
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

Imports System.Web.HttpUtility

Class DeleteForm

    Public Page As Page

    Private Sub DeleteForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("delete-title", Page.Name)
        Localize(Me, "delete")
        RemTalk.Text = Msg("delete-rem-talk")
        If Config.Speedy Then
            For Each Item As SpeedyCriterion In Config.SpeedyCriteria.Values
                If Item.Code = "G8" AndAlso Page.IsTalkPage _
                    OrElse (Item.Code <> "G8" AndAlso Item.Code.StartsWith("G")) _
                    OrElse (Item.Code.StartsWith("A") AndAlso Page.Space.Name = "") _
                    OrElse (Item.Code.StartsWith("R") AndAlso Page.Space.Name = "") _
                    OrElse (Item.Code.StartsWith("C") AndAlso Page.Space.Name = "Category") _
                    OrElse (Item.Code.StartsWith("I") AndAlso Page.Space.Name = "Image") _
                    OrElse (Item.Code.StartsWith("P") AndAlso Page.Space.Name = "Portal") _
                    OrElse (Item.Code.StartsWith("T") AndAlso Page.Space.Name = "Template") _
                    OrElse (Item.Code.StartsWith("U") AndAlso Page.Space.Name.StartsWith("User")) _
                    OrElse (Item.Code.StartsWith("ER") AndAlso Config.Project = "pt.wikipedia") _
                    Then Reason.Items.Add(Item.Code & " - " & Item.Description)
            Next Item
        End If

        If Page.SpeedyCriterion IsNot Nothing Then
            SetReason()
        Else
            Dim NewApiRequest As New ApiRequest
            NewApiRequest.ApiQuery = "action=parse&prop=templates&page=" & UrlEncode(Page.Name)
            NewApiRequest.Start(AddressOf GotTemplates)
            Progress.Text = "Checking tags..."
            Throbber.Start()
        End If

        DeletionLog.Page = Page
    End Sub

    Private Sub GotTemplates(ByVal Result As RequestResult)
        Throbber.Stop()

        If Result.Error Then
            Progress.Text = Result.ErrorMessage
            Exit Sub
        End If

        If Result.Text.Contains("<templates>") Then
            For Each Item As String In Split(FindString(Result.Text, "<templates>", "</templates>"), "</tl>")
                Dim Template As String = FindString(FindString(Item, ">"), ":")

                If Template IsNot Nothing AndAlso Template.StartsWith("Db-") _
                    AndAlso Config.SpeedyCriteria.ContainsKey(Template.Substring(3).ToUpper) Then

                    Page.SpeedyCriterion = Config.SpeedyCriteria(Template.Substring(3).ToUpper)
                    Exit For
                End If
            Next Item
        End If

        Progress.Text = ""
        If Page.SpeedyCriterion Is Nothing Then Progress.Text = "" Else SetReason()
    End Sub

    Private Sub SetReason()
        For Each Item As String In Reason.Items
            If Item.StartsWith(Page.SpeedyCriterion.Code) Then
                Reason.SelectedItem = Item
                Exit For
            End If
        Next Item
    End Sub

    Private Sub DeleteForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub OK_Click() Handles OK.Click
        DialogResult = DialogResult.OK

        Dim Summary As String, Criterion As SpeedyCriterion = Nothing

        If Reason.SelectedIndex > -1 Then
            Dim CriterionName As String = Reason.Text.Substring(0, Reason.Text.IndexOfAny(" -".ToCharArray))
            Criterion = Config.SpeedyCriteria(CriterionName)
            Summary = Config.SpeedyDeleteSummary.Replace("$1", "[[WP:SD#" & _
                Criterion.DisplayCode & "|" & Config.SpeedyCriteria(Criterion.DisplayCode).Description & "]]")
        Else
            Summary = Reason.Text
        End If

        Dim NewDeleteRequest As New DeleteRequest
        NewDeleteRequest.Page = Page
        NewDeleteRequest.Summary = Summary

        If Notify.Enabled AndAlso Notify.Checked Then
            NewDeleteRequest.NotifyRequest = New UserMessageRequest
            NewDeleteRequest.NotifyRequest.AvoidText = Page.Name
            NewDeleteRequest.NotifyRequest.Message = Criterion.Message.Replace("$1", Page.Name)
            NewDeleteRequest.NotifyRequest.Summary = Config.SpeedyMessageSummary.Replace("$1", Page.Name)
            NewDeleteRequest.NotifyRequest.Title = Config.SpeedyMessageTitle.Replace("$1", Page.Name)
        End If

        NewDeleteRequest.Start()

        'Delete talk
        Dim TalkPageDeleteRe As New DeleteRequest
        If RemTalk.Checked = True And Page.IsTalkPage = False And Page.TalkPage.Exists Then

            TalkPageDeleteRe.Page = Page.TalkPage
            TalkPageDeleteRe.Summary = Config.AssociatedDeletion
            TalkPageDeleteRe.Start()
        End If

        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub DeleteForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub Reason_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Reason.KeyDown
        If e.KeyCode = Keys.Enter Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Reason_SelectedIndexChanged() Handles Reason.SelectedIndexChanged
        Notify.Enabled = (Reason.SelectedIndex > -1)
        If Notify.Enabled Then Notify.Checked = _
            Config.SpeedyCriteria(Reason.Text.Substring(0, Reason.Text.IndexOfAny(" -".ToCharArray))).Notify
    End Sub

End Class