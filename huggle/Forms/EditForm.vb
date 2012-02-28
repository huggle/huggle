'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for edit form
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
Imports System.IO

Class EditForm

    Public Page As Page

    Private PreviewCurrent, DiffCurrent, Undoing As Boolean

    Private Sub EditForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("edit-title", Page.Name)
        PageText.SetHighlighting(True)
        Localize(Me, "edit")
        EditTab.Text = Msg("edit-edittab")
        PreviewTab.Text = Msg("edit-previewtab")
        ChangesTab.Text = Msg("edit-changestab")

        Summary.Text = "" 'Blank because we don't want to have the default summary there
        Minor.Checked = Config.Minor("manual")
        Watch.Checked = (Watchlist.Contains(Page) OrElse Config.Watch("manual"))
        WaitMessage.Text = "Retrieving page text..."
        EditPaste.Enabled = Clipboard.ContainsText

        If Page.Text Is Nothing Then
            Dim NewRequest As New PageTextRequest
            NewRequest.Page = Page
            NewRequest.Start(AddressOf GotText)
        Else
            GotText(New RequestResult(Page.Text))
        End If
    End Sub

    Private Sub EditForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub EditForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape : Close()
            Case Keys.F : If e.Modifiers = Keys.Control Then EditFind_Click()
            Case Keys.F6 : ViewSyntaxColoring_Click()
            Case Keys.S : If e.Modifiers = Keys.Control Then SavePage()
        End Select
    End Sub

    Private Sub GotText(ByVal Result As RequestResult)
        If Visible Then
            If Result.Error Then
                WaitMessage.Text = Result.ErrorMessage
            Else
                PageText.Focus()
                WaitMessage.Visible = False
                PageText.Enabled = True
                Summary.Enabled = True
                Minor.Enabled = True
                Watch.Enabled = True
                Save.Enabled = True
                PageText.SetText(Result.Text)
            End If
        End If
    End Sub

    Private Sub SavePage() Handles Save.Click, PageSave.Click
        PageText.Enabled = False
        Summary.Enabled = False
        Minor.Enabled = False
        Watch.Enabled = False
        Save.Enabled = False
        Cancel.Text = Msg("close")
        WaitMessage.Text = "Saving page..."
        WaitMessage.Visible = True

        Dim NewEditRequest As New EditRequest
        NewEditRequest.Minor = Minor.Checked
        NewEditRequest.Watch = Watch.Checked
        NewEditRequest.Summary = Summary.Text
        NewEditRequest.Page = Page
        NewEditRequest.Text = PageText.Text
        NewEditRequest.NoAutoSummary = True
        NewEditRequest.Start(AddressOf Saved)
    End Sub

    Private Sub Saved(ByVal Result As RequestResult)
        If Result.Error Then
            WaitMessage.Text = Result.ErrorMessage
        Else
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Private Sub Summary_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Summary.KeyDown
        If e.KeyCode = Keys.Enter Then SavePage()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Tabs_SelectedIndexChanged() Handles Tabs.SelectedIndexChanged
        If Tabs.SelectedIndex = 1 AndAlso Not PreviewCurrent Then
            Preview.DocumentText = "<div style=""font-family: Arial"">Retrieving preview...</div>"

            Dim NewPreviewRequest As New PreviewRequest
            NewPreviewRequest.Page = Page
            NewPreviewRequest.Text = PageText.Text
            NewPreviewRequest.Start(AddressOf GotPreview)

        ElseIf Tabs.SelectedIndex = 2 AndAlso Not DiffCurrent Then
            Diff.DocumentText = "<div style=""font-family: Arial"">Retrieving diff...</div>"

            Dim NewPreviewRequest As New ChangesRequest
            NewPreviewRequest.Page = Page
            NewPreviewRequest.Text = PageText.Text
            NewPreviewRequest.Start(AddressOf GotDiff)
        End If
    End Sub

    Private Sub GotPreview(ByVal Result As RequestResult)
        If Visible AndAlso Preview IsNot Nothing Then
            If Result.Error Then
                Preview.DocumentText = "<div style=""font-family: Arial"">" & Result.ErrorMessage & "</div>"
            Else
                Preview.DocumentText = MakeHtmlWikiPage(Page.Name, Result.Text)
                PreviewCurrent = True
            End If
        End If
    End Sub

    Private Sub GotDiff(ByVal Result As RequestResult)
        If Visible AndAlso Diff IsNot Nothing Then
            If Result.Error Then
                Diff.DocumentText = "<div style=""font-family: Arial"">" & Result.ErrorMessage & "</div>"
            Else
                Diff.DocumentText = MakeHtmlWikiPage(Page.Name, Result.Text)
                DiffCurrent = True
            End If
        End If
    End Sub

    Private Sub PageText_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles PageText.KeyDown
        If KeystrokeTimer.Enabled Then
            KeystrokeTimer.Stop()
            KeystrokeTimer.Start()
        End If
    End Sub

    Private Sub PageText_TextChanged() Handles PageText.TextChanged
        PreviewCurrent = False
        DiffCurrent = False

        If Not Undoing Then
            If CanUndo() AndAlso UndoActionName() = "Typing" _
                Then ModifyUndoItem(PageText.Text) _
                Else AddUndoItem(PageText.Text, "Typing")

            RefreshUndo()
        End If
    End Sub

    Private Sub PageText_LinkClicked(ByVal s As Object, ByVal e As LinkClickedEventArgs) Handles PageText.LinkClicked
        OpenUrlInBrowser(e.LinkText)
    End Sub

    Private Sub Browser_Navigating(ByVal s As Object, ByVal e As WebBrowserNavigatingEventArgs) _
        Handles Preview.Navigating, Diff.Navigating

        e.Cancel = (e.Url.ToString <> "about:blank")
    End Sub

    Private Sub PageSaveToFile_Click() Handles PageSaveToFile.Click
        Dim NewSaveFileDialog As New SaveFileDialog
        NewSaveFileDialog.Title = "Save " & Page.Name & " to file"
        NewSaveFileDialog.FileName = Page.Name.Replace(":", "-").Replace("/", "-").Replace("\", "-") _
            .Replace("*", "").Replace("?", "").Replace("""", "'") & ".txt"
        NewSaveFileDialog.Filter = "Text file|*.txt"

        Dim Result As DialogResult

        Try
            Result = NewSaveFileDialog.ShowDialog
            If Result = DialogResult.OK Then File.WriteAllText(NewSaveFileDialog.FileName, PageText.Text)

        Catch ex As IOException
            MessageBox.Show("Cannot save file to selected destination: " & ex.Message)
        End Try
    End Sub

    Private Sub PageCancel_Click() Handles PageCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub EditCut_Click() Handles EditCut.Click
        AddUndoItem(PageText.Text, Msg("edit-cut"))
        PageText.Cut()
        EditPaste.Enabled = Clipboard.ContainsText
    End Sub

    Private Sub EditCopy_Click() Handles EditCopy.Click
        PageText.Copy()
        EditPaste.Enabled = Clipboard.ContainsText
    End Sub

    Private Sub EditPaste_Click() Handles EditPaste.Click
        AddUndoItem(PageText.Text, Msg("edit-paste"))
        PageText.Paste()
    End Sub

    Private Sub EditDelete_Click() Handles EditDelete.Click
        AddUndoItem(PageText.Text, Msg("edit-delete"))
        PageText.Text = PageText.Text.Substring(0, PageText.SelectionStart) & _
            PageText.Text.Substring(PageText.SelectionStart + PageText.SelectionLength)
    End Sub

    Private Sub ViewSyntaxColoring_Click() Handles ViewSyntax.Click
        PageText.SetHighlighting(ViewSyntax.Checked)
    End Sub

    Private Sub PageText_SelectionChanged() Handles PageText.SelectionChanged
        EditCut.Enabled = (PageText.SelectionLength > 0)
        EditCopy.Enabled = (PageText.SelectionLength > 0)
        EditDelete.Enabled = (PageText.SelectionLength > 0)
    End Sub

    Private Sub EditUndo_Click() Handles EditUndo.Click
        If CanUndo() Then
            Undo()
            RefreshUndo()
        End If
    End Sub

    Private Sub EditRedo_Click() Handles EditRedo.Click
        If CanRedo() Then
            Redo()
            RefreshUndo()
        End If
    End Sub

    Private Sub RefreshUndo()
        If CanUndo() Then
            EditUndo.Enabled = True
            EditUndo.Text = Msg("edit-edit-undo") & " " & UndoActionName()
        Else
            EditUndo.Enabled = False
            EditUndo.Text = Msg("edit-edit-undo")
        End If

        If CanRedo() Then
            EditRedo.Enabled = True
            EditRedo.Text = Msg("edit-edit-redo") & " " & RedoActionName()
        Else
            EditRedo.Enabled = False
            EditRedo.Text = Msg("edit-edit-redo")
        End If
    End Sub

    Private Sub EditSelectAll_Click() Handles EditSelectAll.Click
        PageText.SelectAll()
    End Sub

    Private Sub EditFind_Click() Handles EditFind.Click
        Find.Focus()
    End Sub

    Private Sub FindText_TextChanged() Handles Find.TextChanged
        Dim Index As Integer = -1

        If Find.Text.Length > 0 Then
            Index = PageText.Text.IndexOf(Find.Text, PageText.SelectionStart, _
                If(MatchCase.Checked, StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase))

            If Index = -1 Then Index = PageText.Text.IndexOf(Find.Text)
            If Index > -1 Then FindInfo.Text = ""
        Else
            FindInfo.Text = ""
        End If

        FindNext.Enabled = (Find.Text.Length > 0)
        FindPrevious.Enabled = (Find.Text.Length > 0)
        ShowFindResult(Index)
    End Sub

    Private Sub FindNext_Click() Handles FindNext.Click
        Dim Index As Integer = PageText.Text.IndexOf(Find.Text, PageText.SelectionStart + 1, _
            If(MatchCase.Checked, StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase))

        If Index = -1 Then
            'Reached end of page, continue from top
            Index = PageText.Text.IndexOf(Find.Text, _
                If(MatchCase.Checked, StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase))
            If Index > -1 Then FindInfo.Text = Msg("edit-searchwrapstart")
        Else
            FindInfo.Text = ""
        End If

        ShowFindResult(Index)
    End Sub

    Private Sub FindPrevious_Click() Handles FindPrevious.Click
        Dim Index As Integer = PageText.Text.LastIndexOf(Find.Text, _
            If(PageText.SelectionStart = 0, PageText.Text.Length - 1, PageText.SelectionStart - 1), _
            If(MatchCase.Checked, StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase))

        If Index = -1 Then
            'Reached top of page, continue from end
            Index = PageText.Text.LastIndexOf(Find.Text, _
                If(MatchCase.Checked, StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase))
            If Index > -1 Then FindInfo.Text = Msg("edit-searchwrapend")
        Else
            FindInfo.Text = ""
        End If

        ShowFindResult(Index)
    End Sub

    Private Sub ShowFindResult(ByVal Index As Integer)
        If Index = -1 Then
            PageText.Select(0, 0)
            ReplaceAll.Enabled = False
        Else
            PageText.Select(Index, Find.Text.Length)
            ReplaceAll.Enabled = True
        End If

        If Find.Text.Length = 0 Or Index > -1 Then
            Find.BackColor = Color.FromKnownColor(KnownColor.Window)
            Find.ForeColor = Color.FromKnownColor(KnownColor.WindowText)
        Else
            FindInfo.Text = Msg("edit-searchnotfound")
            Find.BackColor = Color.LightCoral
            Find.ForeColor = Color.White
        End If
    End Sub

    Private Sub ReplaceB_Click() Handles ReplaceAll.Click
        PageText.Text = PageText.Text.Replace(Find.Text, Replace.Text)
    End Sub

    'RichTextBox has a built-in undo stack, but setting the contents directly clears it
    'Thus we need to use our own, in order that syntax highlighting can be done

    Private UndoItems As New List(Of UndoItem), UndoIndex As Integer = -1

    Private Function CanUndo() As Boolean
        Return (UndoIndex > 0)
    End Function

    Private Function CanRedo() As Boolean
        Return (UndoIndex > 0 AndAlso UndoIndex < UndoItems.Count - 1)
    End Function

    Private Sub Undo()
        UndoIndex -= 1
        Undoing = True
        PageText.SetText(UndoItems(UndoIndex).Text)
        Undoing = False
    End Sub

    Private Sub Redo()
        UndoIndex += 1
        Undoing = True
        PageText.SetText(UndoItems(UndoIndex).Text)
        Undoing = False
    End Sub

    Private Function UndoActionName() As String
        If UndoIndex > UndoItems.Count - 1 Then UndoIndex = UndoItems.Count - 1
        Return UndoItems(UndoIndex).ActionName
    End Function

    Private Function RedoActionName() As String
        If UndoIndex > UndoItems.Count - 1 Then
            UndoIndex = UndoItems.Count - 1
            Return Nothing
        End If

        Return UndoItems(UndoIndex + 1).ActionName
    End Function

    Private Sub AddUndoItem(ByVal Text As String, ByVal ActionName As String)
        While UndoItems.Count > UndoIndex + 1
            UndoItems.RemoveAt(UndoIndex + 1)
        End While

        Dim NewItem As New Undoitem
        NewItem.Text = Text
        NewItem.ActionName = ActionName
        UndoItems.Add(NewItem)
        UndoIndex += 1
    End Sub

    Private Sub ModifyUndoItem(ByVal Text As String)
        While UndoItems.Count > UndoIndex + 1
            UndoItems.RemoveAt(UndoIndex + 1)
        End While

        UndoItems(UndoIndex).Text = Text
    End Sub

    Private Class UndoItem

        Public Text As String
        Public ActionName As String

    End Class

    Private Sub EditForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PageText_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageText.TextChanged

    End Sub

    Private Sub PageText_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PageText.SelectionChanged

    End Sub
End Class
