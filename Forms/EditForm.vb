Imports System.IO

Class EditForm

    Public Page As Page

    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hWnd As IntPtr) As Integer
    Private WithEvents Timer As New Timer
    Private HighlightTimeout As Integer = 2000
    Private PreviewCurrent, DiffCurrent, SettingText, Undoing As Boolean, CurrentRequest As HighlightRequest

    Private Sub EditForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        Text = Msg("edit-title", Page.Name)
        Localize(Me, "edit")
        EditTab.Text = Msg("edit-edittab")
        PreviewTab.Text = Msg("edit-previewtab")
        ChangesTab.Text = Msg("edit-changestab")

        Summary.Text = Config.DefaultSummary
        Minor.Checked = Config.Minor("manual")
        Watch.Checked = (Watchlist.Contains(Page) OrElse Config.Watch("manual"))
        WaitMessage.Text = "Retrieving page text..."
        EditPaste.Enabled = Clipboard.ContainsText

        For Each Item As FontFamily In (New System.Drawing.Text.InstalledFontCollection).Families
            If Item.Name = "Consolas" Then
                PageText.Font = New Font(Item, PageText.Font.Size)
                Highlight.FontName = Highlight.FontName.Replace("Courier New", "Consolas")
                Exit For
            End If
        Next Item

        PageText.Rtf = Highlight.RtfHeader & Highlight.RtfFooter

        If Page.Text Is Nothing Then
            Dim NewGetTextRequest As New PageTextRequest
            NewGetTextRequest.Page = Page
            NewGetTextRequest.Start(AddressOf GotText)
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
                SettingText = True
                PageText.Text = Result.Text
                DoHighlight()
                SettingText = False
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
        If Not SettingText Then
            PreviewCurrent = False
            DiffCurrent = False
            KeystrokeTimer.Stop()
            KeystrokeTimer.Start()

            If Not Undoing Then
                If CanUndo() AndAlso UndoActionName() = "Typing" _
                    Then ModifyUndoItem(PageText.Text) _
                    Else AddUndoItem(PageText.Text, "Typing")

                RefreshUndo()
            End If
        End If
    End Sub

    Private Sub PageText_LinkClicked(ByVal s As Object, ByVal e As LinkClickedEventArgs) Handles PageText.LinkClicked
        OpenUrlInBrowser(e.LinkText)
    End Sub

    Private Sub DoHighlight()
        If ViewSyntax.Checked Then
            Timer.Interval = HighlightTimeout
            Timer.Start()

            CurrentRequest = New HighlightRequest
            CurrentRequest.Start(PageText.Text, AddressOf HighlightDone)
        End If
    End Sub

    Private Sub KeystrokeTimer_Tick() Handles KeystrokeTimer.Tick
        KeystrokeTimer.Stop()
        DoHighlight()
    End Sub

    Private Sub HighlightDone(ByVal Result As String)
        Timer.Stop()

        Dim Pos As Integer = PageText.SelectionStart
        Dim Length As Integer = PageText.SelectionLength
        Dim StartOfLowestLine As Integer = PageText.GetFirstCharIndexFromLine(PageText.GetLineFromCharIndex _
            (Math.Max(0, PageText.GetCharIndexFromPosition(New Point(0, PageText.Height))) - 1))

        LockWindowUpdate(PageText.Handle)
        SettingText = True
        PageText.Rtf = Result
        SettingText = False
        PageText.SelectionStart = StartOfLowestLine
        PageText.SelectionLength = 1
        PageText.SelectionStart = Pos
        PageText.SelectionLength = Length
        LockWindowUpdate(IntPtr.Zero)
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
        DialogResult = Windows.Forms.DialogResult.Cancel
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
        If ViewSyntax.Checked Then
            DoHighlight()
        Else
            SettingText = True

            Dim Source As String = PageText.Text
            Source = Highlight.RtfEscape(Source)
            Source = Highlight.RtfHeader & Source
            Source &= Highlight.RtfFooter
            PageText.Rtf = Source

            SettingText = False
        End If
    End Sub

    Private Sub PageText_SelectionChanged() Handles PageText.SelectionChanged
        EditCut.Enabled = (PageText.SelectionLength > 0)
        EditCopy.Enabled = (PageText.SelectionLength > 0)
        EditDelete.Enabled = (PageText.SelectionLength > 0)
    End Sub

    Private Sub EditUndo_Click() Handles EditUndo.Click
        If CanUndo() Then
            Undo()
            DoHighlight()
            RefreshUndo()
        End If
    End Sub

    Private Sub EditRedo_Click() Handles EditRedo.Click
        If CanRedo() Then
            Redo()
            DoHighlight()
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

    Private Sub Timer_Tick() Handles Timer.Tick
        ViewSyntax.Checked = False
        CurrentRequest.Cancel()
        Timer.Stop()
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
        PageText.Text = UndoItems(UndoIndex).Text
        Undoing = False
    End Sub

    Private Sub Redo()
        UndoIndex += 1
        Undoing = True
        PageText.Text = UndoItems(UndoIndex).Text
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

End Class
