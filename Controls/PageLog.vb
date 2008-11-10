Class PageLog

    'Control for displaying page log

    Inherits ListView2

    Private _Page As Page, _Mode As ViewMode

    Public Property Page() As Page
        Get
            Return _Page
        End Get
        Set(ByVal value As Page)
            _Page = value
            RefreshLog()
        End Set
    End Property

    Public Property Mode() As ViewMode
        Get
            Return _Mode
        End Get
        Set(ByVal value As ViewMode)
            _Mode = value
            RefreshLog()
        End Set
    End Property

    Public Sub RefreshLog()
        Clear()

        If Page IsNot Nothing Then
            Columns.Add("", Width - 10)

            If Mode = ViewMode.Delete Then
                Items.Add(Msg("deletelog-progress"))

                Dim NewRequest As New DeleteLogRequest
                NewRequest.Page = Page
                NewRequest.Start(AddressOf DeletionRequestDone)

            ElseIf Mode = ViewMode.Protect Then
                Items.Add(Msg("protectlog-progress"))

                Dim NewRequest As New ProtectionLogRequest
                NewRequest.Page = Page
                NewRequest.Start(AddressOf ProtectionRequestDone)
            End If
        End If
    End Sub

    Private Sub DeletionRequestDone(ByVal Result As RequestResult)
        If Result.Error Then
            Clear()
            Columns.Add("", Width - 10)
            Items.Add(Result.ErrorMessage)

        ElseIf Page.Deletes Is Nothing OrElse Page.Deletes.Count = 0 Then
            Clear()
            Columns.Add("", Width - 10)
            Items.Add(Msg("deletelog-none", Page.Name))

        Else
            Items.Clear()
            Columns.Clear()
            Columns.Add("Date", 110)
            Columns.Add("Action", 50)
            Columns.Add("Admin", 100)
            Columns.Add("Comment", 200)

            For Each Item As Delete In Page.Deletes
                Dim NewListViewItem As New ListViewItem

                NewListViewItem.Text = Item.Time.ToShortDateString & " " & Item.Time.ToShortTimeString
                NewListViewItem.SubItems.Add(Item.Action)
                NewListViewItem.SubItems.Add(Item.Admin.Name)
                NewListViewItem.SubItems.Add(TrimSummary(Item.Comment))

                Items.Add(NewListViewItem)
            Next Item
        End If
    End Sub

    Private Sub ProtectionRequestDone(ByVal Result As RequestResult)
        If Result.Error Then
            Clear()
            Columns.Add("", Width)
            Items.Add(Result.ErrorMessage)

        ElseIf Page.Deletes Is Nothing OrElse Page.Deletes.Count = 0 Then
            Clear()
            Columns.Add("", Width)
            Items.Add(Msg("protectlog-none", Page.Name))

        Else
            Items.Clear()
            Columns.Clear()
            Columns.Add("Date", 110)
            Columns.Add("Admin", 100)
            Columns.Add("Edit", 50)
            Columns.Add("Move", 50)
            Columns.Add("Expiry", 110)
            Columns.Add("Comment", 120)

            For Each Item As Protection In Page.Protections
                Dim NewListViewItem As New ListViewItem

                NewListViewItem.Text = Item.Time.ToShortDateString & " " & Item.Time.ToShortTimeString
                NewListViewItem.SubItems.Add(Item.Admin.Name)
                NewListViewItem.SubItems.Add(Item.EditLevel.Replace("autoconfirmed", "autoc."))
                NewListViewItem.SubItems.Add(Item.MoveLevel.Replace("autoconfirmed", "autoc."))
                If Item.EditExpiry = Date.MinValue Then NewListViewItem.SubItems.Add("-") _
                    Else NewListViewItem.SubItems.Add(Item.EditExpiry.ToShortDateString & " " & _
                    Item.EditExpiry.ToShortTimeString)
                NewListViewItem.SubItems.Add(TrimSummary(Item.Summary))

                Items.Add(NewListViewItem)
            Next Item
        End If
    End Sub

    Public Enum ViewMode As Integer
        None = 0
        Delete = 1
        Protect = 2
    End Enum

End Class
