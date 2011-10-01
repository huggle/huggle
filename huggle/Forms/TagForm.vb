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


Class TagForm

    Public Page As Page

    Private Sub TagForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("tag-title", Page.Name)
        Localize(Me, "tag")

        ToSpeedy.Visible = Config.Speedy
        ToProd.Visible = Config.Prod

        TagSelector.Items.AddRange(Config.Tags.ToArray)
        Tags.Text = LastTagText
        Tags.Focus()
        Tags.SelectAll()
    End Sub

    Private Sub OK_Click() Handles OK.Click
        If Tags.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub TagForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel

        If DialogResult = DialogResult.OK Then
            LastTagText = Tags.Text
            If Summary.Text = "" Then Summary.Text = Tags.Text.Replace(LF, " ")

            Dim NewRequest As New TagRequest

            NewRequest.Page = CurrentEdit.Page
            NewRequest.Summary = Summary.Text
            NewRequest.Tag = Tags.Text
            NewRequest.InsertAtEnd = InsertAtEnd.Checked
            NewRequest.Start()
        End If
    End Sub

    Private Sub Summary_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Summary.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Tags.Text <> "" Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub TagForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub TagText_TextChanged() Handles Tags.TextChanged
        OK.Enabled = (Tags.Text <> "")
        Summary.Text = Tags.Text.Replace(LF, " ")
        If Summary.Text.Length > 250 Then Summary.Text = Summary.Text.Substring(0, 250)
    End Sub

    Private Sub TagSelector_SelectedIndexChanged() Handles TagSelector.SelectedIndexChanged
        If Not Tags.Text.Contains(CStr(TagSelector.Items(TagSelector.SelectedIndex))) Then
            If Tags.Text <> "" Then Tags.Text &= LF
            Tags.Text &= CStr(TagSelector.Items(TagSelector.SelectedIndex))
        End If
    End Sub

    Private Sub Explanation_LinkClicked() Handles Explanation.LinkClicked
        Dim NewEditForm As New EditForm
        NewEditForm.Page = Page
        NewEditForm.Show()

        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ToSpeedy_Click() Handles ToSpeedy.Click
        DialogResult = DialogResult.Cancel
        Close()
        MainForm.TagSpeedy_Click()
    End Sub

    Private Sub ToProd_Click() Handles ToProd.Click
        DialogResult = DialogResult.Cancel
        Close()
        MainForm.PageTagProd_Click()
    End Sub

    Private Sub TagText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tags.TextChanged

    End Sub
End Class