'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code
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

Class XfdForm

    Public Page As Page

    Private Sub XfdForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("xfd-title", Page.Name)
        Localize(Me, "xfd")

        Category.Visible = Page.IsArticle
        CategoryLabel.Visible = Page.IsArticle
        If Category.Visible Then Category.SelectedIndex = 0

        If Page.Space Is Space.Article Then
            NominationType.Text = "Article"
        ElseIf Page.Space Is Space.Category Then
            NominationType.Text = "Category"
        ElseIf Page.Space Is Space.Image Then
            NominationType.Text = "Image"
        Else
            NominationType.Text = "Miscellaneous"
        End If
    End Sub

    Private Sub XfdForm_FormClosing() Handles MyBase.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub XfdForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub Reason_TextChanged() Handles Reason.TextChanged
        OK.Enabled = (Reason.Text <> "")
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Page.Space Is Space.Article Then
            Dim NewRequest As New AfdRequest
            NewRequest.Category = Category.Text.Substring(0, 1).Replace("(", "?")
            NewRequest.Page = Page
            NewRequest.Reason = Reason.Text
            NewRequest.Notify = Notify.Checked
            NewRequest.Start()

        ElseIf Page.Space Is Space.Category Then
            Dim NewRequest As New CfdRequest
            NewRequest.Page = Page
            NewRequest.Reason = Reason.Text
            NewRequest.Notify = Notify.Checked
            NewRequest.Start()

        ElseIf Page.Space Is Space.Template Then
            Dim NewRequest As New TfdRequest
            NewRequest.Page = Page
            NewRequest.Reason = Reason.Text
            NewRequest.Notify = Notify.Checked
            NewRequest.Start()

        ElseIf Page.Space Is Space.Image Then
            Dim NewRequest As New IfdRequest
            NewRequest.Page = Page
            NewRequest.Reason = Reason.Text
            NewRequest.Notify = Notify.Checked
            NewRequest.Start()

        Else
            Dim NewRequest As New MfdRequest
            NewRequest.Page = Page
            NewRequest.Reason = Reason.Text
            NewRequest.Notify = Notify.Checked
            NewRequest.Start()
        End If

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Notify_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Notify.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Reason.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

End Class