Imports System.IO

Class EditForm

    Public Page As Page

    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hWnd As IntPtr) As Integer
    Private PreviewCurrent, SettingText As Boolean

    Private Sub EditForm_Load() Handles Me.Load
        Text = "Editing " & Page.Name
        Summary.Text = Config.DefaultSummary
        Minor.Checked = Config.MinorOther
        Watch.Checked = (Watchlist.Contains(Page) OrElse Config.WatchOther)
        WaitMessage.Text = "Retrieving page text..."

        For Each Item As FontFamily In (New System.Drawing.Text.InstalledFontCollection).Families
            If Item.Name = "Consolas" Then
                PageText.Font = New Font(Item, PageText.Font.Size)
                Highlight.FontName = Highlight.FontName.Replace("Courier New", "Consolas")
                Exit For
            End If
        Next Item

        Dim NewGetTextRequest As New GetTextRequest
        NewGetTextRequest.Page = Page
        NewGetTextRequest.Start(AddressOf GotText)
    End Sub

    Private Sub EditForm_FormClosing() Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub

    Private Sub EditForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub GotText(ByVal Result As Boolean, ByVal Text As String)
        If Result Then
            PageText.Focus()
            WaitMessage.Visible = False
            PageText.Enabled = True
            Summary.Enabled = True
            Minor.Enabled = True
            Watch.Enabled = True
            Save.Enabled = True

            SettingText = True
            PageText.Text = Text
            DoHighlight()
            SettingText = False
        Else
            WaitMessage.Text = "Failed to retrieve page text"
        End If
    End Sub

    Private Sub SavePage() Handles Save.Click, PageSave.Click
        PageText.Enabled = False
        Summary.Enabled = False
        Minor.Enabled = False
        Watch.Enabled = False
        Save.Enabled = False
        Cancel.Text = "Close"
        WaitMessage.Text = "Saving page..."
        WaitMessage.Visible = True

        Dim NewEditRequest As New EditRequest
        NewEditRequest.Minor = Minor.Checked
        NewEditRequest.Watch = Watch.Checked
        NewEditRequest.Summary = Summary.Text
        NewEditRequest.Page = Page
        NewEditRequest.Text = PageText.Text
        NewEditRequest.Start(AddressOf Saved)
    End Sub

    Private Sub Saved(ByVal Result As Boolean)
        If Result Then
            DialogResult = DialogResult.OK
            Close()
        Else
            WaitMessage.Text = "Failed to save page"
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
        End If
    End Sub

    Private Sub GotPreview(ByVal Result As Boolean, ByVal Text As String)
        If Preview IsNot Nothing Then
            If Result Then
                Preview.DocumentText = FormatPageHtml(Page, Text)
                PreviewCurrent = True
            Else
                Preview.DocumentText = "<div style=""font-family: Arial"">Failed to retrieve preview</div>"
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
            KeystrokeTimer.Stop()
            KeystrokeTimer.Start()
        End If
    End Sub

    Private Sub PageText_LinkClicked(ByVal s As Object, ByVal e As LinkClickedEventArgs) Handles PageText.LinkClicked
        Process.Start(e.LinkText)
    End Sub

    Private Sub DoHighlight()
        Dim NewHighlightRequest As New HighlightRequest
        NewHighlightRequest.Start(PageText.Text, AddressOf HighlightDone)
    End Sub

    Private Sub KeystrokeTimer_Tick() Handles KeystrokeTimer.Tick
        KeystrokeTimer.Stop()
        DoHighlight()
    End Sub

    Private Sub HighlightDone(ByVal Result As String)
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

    Private Sub Apply_Click()
        Cancel.Text = "Close"

        Dim NewEditRequest As New EditRequest
        NewEditRequest.Minor = Minor.Checked
        NewEditRequest.Watch = Watch.Checked
        NewEditRequest.Summary = Summary.Text
        NewEditRequest.Page = Page
        NewEditRequest.Text = PageText.Text
        NewEditRequest.Start()
    End Sub

    Private Sub Preview_Navigating(ByVal s As Object, ByVal e As WebBrowserNavigatingEventArgs) _
        Handles Preview.Navigating

        e.Cancel = (e.Url.ToString <> "about:blank")
    End Sub

End Class