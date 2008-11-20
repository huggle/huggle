Imports System.IO
Imports System.Text.RegularExpressions

<DebuggerDisplay("{Name}")> _
Class Queue

    'Represents a queue of revisions

    Public Shared All As New Dictionary(Of String, Queue)
    Public Shared [Default], SecondDefault As Queue

    Private Items As SortedList(Of Edit)
    Private WithEvents RefreshTimer As New Timer

    Private _FilterAnonymous As QueueFilter = QueueFilter.None
    Private _FilterAssisted As QueueFilter = QueueFilter.None
    Private _FilterBot As QueueFilter = QueueFilter.None
    Private _FilterHuggle As QueueFilter = QueueFilter.None
    Private _FilterIgnored As QueueFilter = QueueFilter.None
    Private _FilterMe As QueueFilter = QueueFilter.None
    Private _FilterNewPage As QueueFilter = QueueFilter.None
    Private _FilterNotifications As QueueFilter = QueueFilter.None
    Private _FilterOwnUserspace As QueueFilter = QueueFilter.None
    Private _FilterReverts As QueueFilter = QueueFilter.None
    Private _FilterTags As QueueFilter = QueueFilter.None
    Private _FilterWarnings As QueueFilter = QueueFilter.None

    Private _Diffs As DiffMode
    Private _Limit As Integer
    Private _SourceType As String
    Private _Source As String
    Private _IgnorePages As Boolean
    Private _ListName As String
    Private _Name As String
    Private _NeedsReset As Boolean
    Private _PageRegex As Regex
    Private _Pages As List(Of String)
    Private _Refreshing As Boolean
    Private _RefreshAlways As Boolean
    Private _RefreshReAdd As Boolean
    Private _RemovedPages As List(Of String)
    Private _RemoveOld As Boolean
    Private _RemoveAfter As Integer
    Private _RemoveViewed As Boolean
    Private _RevisionRegex As Regex
    Private _SortOrder As QueueSortOrder
    Private _Spaces As New List(Of Space)
    Private _SummaryRegex As Regex
    Private _TrayNotification As Boolean
    Private _Type As QueueType
    Private _UserRegex As Regex
    Private _Users As New List(Of String)

    Public Sub New(ByVal Name As String)
        _Name = Name
        _Diffs = DiffMode.None
        _IgnorePages = True
        _RemoveViewed = True
        _Spaces.AddRange(Space.All)
        _Type = QueueType.Live
        _RefreshTimer.Interval = 30000
        _Limit = 500
        _RefreshReAdd = True
        If Not QueueNames.ContainsKey(Config.Project) Then QueueNames(Config.Project) = New List(Of String)
        If Not QueueNames(Config.Project).Contains(Name) Then QueueNames(Config.Project).Add(Name)
        All.Add(Name, Me)
    End Sub

    Public ReadOnly Property Edits() As SortedList(Of Edit)
        Get
            If _NeedsReset OrElse Items Is Nothing Then Reset()
            Return Items
        End Get
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            All.Remove(_Name)
            _Name = value
            All.Add(value, Me)
        End Set
    End Property

    Public Property DiffMode() As DiffMode
        Get
            Return _Diffs
        End Get
        Set(ByVal value As DiffMode)
            _Diffs = value
            _NeedsReset = True
        End Set
    End Property

    Public Property DynamicSource() As String
        Get
            Return _Source
        End Get
        Set(ByVal value As String)
            _Source = value
        End Set
    End Property

    Public Property DynamicSourceType() As String
        Get
            Return _SourceType
        End Get
        Set(ByVal value As String)
            _SourceType = value
        End Set
    End Property

    Public Property FilterAnonymous() As QueueFilter
        Get
            Return _FilterAnonymous
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterAnonymous Then _NeedsReset = True
            _FilterAnonymous = value
        End Set
    End Property

    Public Property FilterAssisted() As QueueFilter
        Get
            Return _FilterAssisted
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterAssisted Then _NeedsReset = True
            _FilterAssisted = value
        End Set
    End Property

    Public Property FilterBot() As QueueFilter
        Get
            Return _FilterBot
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterBot Then _NeedsReset = True
            _FilterBot = value
        End Set
    End Property

    Public Property FilterHuggle() As QueueFilter
        Get
            Return _FilterHuggle
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterHuggle Then _NeedsReset = True
            _FilterHuggle = value
        End Set
    End Property

    Public Property FilterIgnored() As QueueFilter
        Get
            Return _FilterIgnored
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterIgnored Then _NeedsReset = True
            _FilterIgnored = value
        End Set
    End Property

    Public Property FilterMe() As QueueFilter
        Get
            Return _FilterMe
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterMe Then _NeedsReset = True
            _FilterMe = value
        End Set
    End Property

    Public Property FilterNewPage() As QueueFilter
        Get
            Return _FilterNewPage
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterNewPage Then _NeedsReset = True
            _FilterNewPage = value
        End Set
    End Property

    Public Property FilterNotifications() As QueueFilter
        Get
            Return _FilterNotifications
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterNotifications Then _NeedsReset = True
            _FilterNotifications = value
        End Set
    End Property

    Public Property FilterOwnUserspace() As QueueFilter
        Get
            Return _FilterOwnUserspace
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterOwnUserspace Then _NeedsReset = True
            _FilterOwnUserspace = value
        End Set
    End Property

    Public Property FilterReverts() As QueueFilter
        Get
            Return _FilterReverts
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterReverts Then _NeedsReset = True
            _FilterReverts = value
        End Set
    End Property

    Public Property FilterTags() As QueueFilter
        Get
            Return _FilterTags
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterTags Then _NeedsReset = True
            _FilterTags = value
        End Set
    End Property

    Public Property FilterWarnings() As QueueFilter
        Get
            Return _FilterWarnings
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterWarnings Then _NeedsReset = True
            _FilterWarnings = value
        End Set
    End Property

    Public Property IgnorePages() As Boolean
        Get
            Return _IgnorePages
        End Get
        Set(ByVal value As Boolean)
            If value <> _IgnorePages Then _NeedsReset = True
            _IgnorePages = value
        End Set
    End Property

    Public Property ListName() As String
        Get
            Return _ListName
        End Get
        Set(ByVal value As String)
            _ListName = value
            If String.IsNullOrEmpty(value) Then _Pages = Nothing Else _Pages = AllLists(_ListName)
            _NeedsReset = True
        End Set
    End Property

    Public Property NeedsReset() As Boolean
        Get
            Return _NeedsReset
        End Get
        Set(ByVal value As Boolean)
            _NeedsReset = value
        End Set
    End Property

    Public Property PageRegex() As Regex
        Get
            Return _PageRegex
        End Get
        Set(ByVal value As Regex)
            If value Is Nothing OrElse _PageRegex Is Nothing OrElse value.ToString <> _PageRegex.ToString _
                Then _NeedsReset = True
            _PageRegex = value
        End Set
    End Property

    Public ReadOnly Property Pages() As List(Of String)
        Get
            Return _Pages
        End Get
    End Property

    Public ReadOnly Property Refreshing() As Boolean
        Get
            Return _Refreshing
        End Get
    End Property

    Public Property RefreshAlways() As Boolean
        Get
            Return _RefreshAlways
        End Get
        Set(ByVal value As Boolean)
            _RefreshAlways = value
        End Set
    End Property

    Public Property RefreshInterval() As Integer
        Get
            Return _RefreshTimer.Interval \ 1000
        End Get
        Set(ByVal value As Integer)
            _RefreshTimer.Interval = value * 1000
        End Set
    End Property

    Public Property RefreshReAdd() As Boolean
        Get
            Return _RefreshReAdd
        End Get
        Set(ByVal value As Boolean)
            _RefreshReAdd = value
        End Set
    End Property

    Public Property RemoveAfter() As Integer
        Get
            Return _RemoveAfter
        End Get
        Set(ByVal value As Integer)
            _RemoveAfter = value
        End Set
    End Property

    Public Property RemoveOld() As Boolean
        Get
            Return _RemoveOld
        End Get
        Set(ByVal value As Boolean)
            _RemoveOld = value
        End Set
    End Property

    Public Property RemoveViewed() As Boolean
        Get
            Return _RemoveViewed
        End Get
        Set(ByVal value As Boolean)
            _RemoveViewed = value
        End Set
    End Property

    Public Property RevisionRegex() As Regex
        Get
            Return _RevisionRegex
        End Get
        Set(ByVal value As Regex)
            If value Is Nothing OrElse _RevisionRegex Is Nothing OrElse value.ToString <> _RevisionRegex.ToString _
                Then _NeedsReset = True
            _RevisionRegex = value
        End Set
    End Property

    Public Property SortOrder() As QueueSortOrder
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As QueueSortOrder)
            If value <> _SortOrder Then _NeedsReset = True
            _SortOrder = value
        End Set
    End Property

    Public Property Spaces() As List(Of Space)
        Get
            Return _Spaces
        End Get
        Set(ByVal value As List(Of Space))
            If value.Count <> _Spaces.Count Then _NeedsReset = True
            _Spaces = value
        End Set
    End Property

    Public Property SummaryRegex() As Regex
        Get
            Return _SummaryRegex
        End Get
        Set(ByVal value As Regex)
            If value Is Nothing OrElse _SummaryRegex Is Nothing OrElse value.ToString <> _SummaryRegex.ToString _
                Then _NeedsReset = True
            _SummaryRegex = value
        End Set
    End Property

    Public Property TrayNotification() As Boolean
        Get
            Return _TrayNotification
        End Get
        Set(ByVal value As Boolean)
            _TrayNotification = value
        End Set
    End Property

    Public Property Type() As QueueType
        Get
            Return _Type
        End Get
        Set(ByVal value As QueueType)
            If value <> _Type Then _NeedsReset = True
            _Type = value
        End Set
    End Property

    Public Property Users() As List(Of String)
        Get
            Return _Users
        End Get
        Set(ByVal value As List(Of String))
            _Users = value
        End Set
    End Property

    Public Property UserRegex() As Regex
        Get
            Return _UserRegex
        End Get
        Set(ByVal value As Regex)
            If value Is Nothing OrElse _UserRegex Is Nothing OrElse value.ToString <> _UserRegex.ToString _
                Then _NeedsReset = True
            _UserRegex = value
        End Set
    End Property

    'Define equality operators so that instances can be used with Case statements

    Public Shared Operator =(ByVal a As Queue, ByVal b As Queue) As Boolean
        Return (a Is b)
    End Operator

    Public Shared Operator <>(ByVal a As Queue, ByVal b As Queue) As Boolean
        Return (a IsNot b)
    End Operator

    Public Sub ForceUpdate()
        If Type = QueueType.Dynamic Then
            RefreshTimer.Stop()
            RefreshTimer_Tick()
            RefreshTimer.Start()
        End If
    End Sub

    Public Sub Reset()
        'Reset a queue - clear a live one, populate a fixed one

        'Define sort order
        Dim Comparer As IComparer(Of Edit)

        If Type = QueueType.FixedList OrElse Type = QueueType.Dynamic Then
            Comparer = New Edit.CompareByPageName
        ElseIf SortOrder = QueueSortOrder.Quality Then
            Comparer = New Edit.CompareByQuality
        ElseIf SortOrder = QueueSortOrder.Time Then
            Comparer = New Edit.CompareByTime
        Else
            Comparer = New Edit.CompareByTimeReverse
        End If

        If Type = QueueType.Dynamic Then
            If Not _RefreshTimer.Enabled Then
                RefreshTimer_Tick()
                _RefreshTimer.Start()
            End If
        Else
            _RefreshTimer.Stop()
        End If

        Items = New SortedList(Of Edit)(Comparer)

        'Populate a fixed queue
        If _Pages IsNot Nothing AndAlso _Type = QueueType.FixedList Then
            For Each Item As String In _Pages
                Dim Page As Page = GetPage(Item)

                If Page.LastEdit IsNot Nothing Then
                    Items.Add(Page.LastEdit)
                Else
                    Dim NewEdit As New Edit
                    NewEdit.Page = Page
                    NewEdit.Id = "cur"
                    NewEdit.Oldid = "prev"
                    Items.Add(NewEdit)
                End If
            Next Item
        End If

        _NeedsReset = False
    End Sub

    Public Sub AddEdit(ByVal Edit As Edit)
        If Items IsNot Nothing Then
            If _Type <> QueueType.FixedList Then RemoveOldEdits(_RemoveAfter)

            'Add edit to the queue
            Items.Add(Edit)
            If Config.TrayIcon AndAlso TrayNotification Then MainForm.TrayIcon.ShowBalloonTip(10000, _
                "'" & Name & "' matched revision", Edit.Page.Name & " edited by " & Edit.User.Name, ToolTipIcon.None)
            If Items.Count > Config.QueueSize Then Items.RemoveAt(Items.Count - 1)
        End If
    End Sub

    Public Sub RemoveOldEdits(ByVal Time As Integer)
        'Remove old edits and edits to the same page
        If _RemoveOld OrElse Time > 0 Then
            Dim i As Integer = 0

            While i < Edits.Count
                If (_RemoveOld AndAlso Edits(i) IsNot Edits(i).Page.LastEdit) _
                    OrElse (_RemoveAfter > 0 AndAlso Edits(i).Time.AddMinutes(Time) < Date.UtcNow) Then

                    Items.RemoveAt(i)
                Else
                    i += 1
                End If
            End While
        End If
    End Sub

    Public Sub RefreshEdit(ByVal Edit As Edit)
        'Remove and possibly re-add an edit with changed properties that might affect sort order
        If Type <> QueueType.FixedList AndAlso Items IsNot Nothing Then
            If Items.Contains(Edit) Then
                Items.Remove(Edit)
                If MatchesFilter(Edit) Then Items.Add(Edit)
            End If
        End If
    End Sub

    Public Sub RemoveEdit(ByVal Edit As Edit)
        'Remove an edit
        If Items IsNot Nothing Then Items.Remove(Edit)
    End Sub

    Public Sub RemoveViewedEdit(ByVal Edit As Edit)
        'Remove an edit that has been viewed, possibly on another queue
        If _RemoveViewed AndAlso Items IsNot Nothing Then Items.Remove(Edit)
        If _Type = QueueType.Dynamic AndAlso Not _RefreshReAdd Then _RemovedPages.Add(Edit.Page.Name)
    End Sub

    Public Function MatchesFilter(ByVal PageName As String) As Boolean
        'Determines whether a page name matches this queue's filter

        If _IgnorePages AndAlso Config.IgnoredPages.Contains(PageName) Then Return False
        If _PageRegex IsNot Nothing AndAlso Not _PageRegex.IsMatch(PageName) Then Return False

        Return True
    End Function

    Public Function MatchesContentFilter(ByVal Edit As Edit) As Boolean
        Return (_RevisionRegex Is Nothing OrElse _RevisionRegex.IsMatch(Edit.ChangedContent))
    End Function

    Public Function MatchesFilter(ByVal Edit As Edit) As Boolean
        'Determines whether an edit matches this queue's filter
        If Edit.Deleted Then Return False

        If _Type = QueueType.FixedList OrElse _Type = QueueType.Dynamic Then Return False
        If _Type = QueueType.LiveList AndAlso (_Pages Is Nothing OrElse Not _Pages.Contains(Edit.Page.Name)) _
            Then Return False
        If _Spaces.Count > 0 AndAlso Not _Spaces.Contains(Edit.Page.Space) Then Return False
        If _PageRegex IsNot Nothing AndAlso Not _PageRegex.IsMatch(Edit.Page.Name) Then Return False
        If _UserRegex IsNot Nothing AndAlso Not _UserRegex.IsMatch(Edit.User.Name) Then Return False
        If _SummaryRegex IsNot Nothing AndAlso Not _SummaryRegex.IsMatch(Edit.Summary) Then Return False
        If _IgnorePages AndAlso Config.IgnoredPages.Contains(Edit.Page.Name) Then Return False
        If _Users.Count > 0 AndAlso Not Users.Contains(Edit.User.Name) Then Return False

        Return QueueFilterMatch(_FilterAnonymous, Edit.User.Anonymous) _
            AndAlso QueueFilterMatch(_FilterAssisted, Edit.Assisted) _
            AndAlso QueueFilterMatch(_FilterBot, Edit.Bot) _
            AndAlso QueueFilterMatch(_FilterHuggle, Edit.IsHuggleEdit) _
            AndAlso QueueFilterMatch(_FilterIgnored, Edit.User.Ignored) _
            AndAlso QueueFilterMatch(_FilterMe, Edit.User.IsMe) _
            AndAlso QueueFilterMatch(_FilterNewPage, Edit.NewPage) _
            AndAlso QueueFilterMatch(_FilterNotifications, Edit.Type = Huggle.EditType.Notification) _
            AndAlso QueueFilterMatch(_FilterOwnUserspace, Edit.OwnUserspace) _
            AndAlso QueueFilterMatch(_FilterReverts, Edit.Type = EditType.Revert) _
            AndAlso QueueFilterMatch(_FilterTags, Edit.Type = EditType.Tag) _
            AndAlso QueueFilterMatch(_FilterWarnings, Edit.Type = EditType.Warning)
    End Function

    Private Function QueueFilterMatch(ByVal Filter As QueueFilter, ByVal Value As Boolean) As Boolean
        'Helper function for above
        Select Case Filter
            Case QueueFilter.None : Return True
            Case QueueFilter.Require : Return Value
            Case QueueFilter.Exclude : Return Not Value
        End Select
    End Function

    Private Sub RefreshTimer_Tick() Handles RefreshTimer.Tick
        If Type = QueueType.Dynamic AndAlso Not _NeedsReset _
            AndAlso (_RefreshAlways OrElse CurrentQueue Is Me OrElse SecondQueue Is Me) Then

            RefreshTimer.Stop()

            Select Case _SourceType
                'This is a list of the sources that can be used when creating a list in the list builder
                'Also shown is the method of retreiving the list
                Case "Backlinks" : GetList(New BacklinksRequest(_Source))
                Case "Category" : GetList(New CategoryRequest(_Source.Replace("Category:", "")))
                Case "Category (recursive)" : GetList(New RecursiveCategoryRequest(_Source.Replace("Category:", "")))
                Case "External link uses" : GetList(New ExternalLinkUsageRequest(_Source.Replace("http://", "")))
                Case "Image uses" : GetList(New ImageUsageRequest(_Source.Replace("Image:", "")))
                Case "Images on page" : GetList(New ImagesRequest(_Source))
                Case "Links on page" : GetList(New LinksRequest(_Source))
                Case "Search" : GetList(New SearchRequest(_Source))
                Case "Templates on page" : GetList(New TemplatesRequest(_Source))
                Case "Transclusions" : GetList(New TransclusionsRequest(_Source))
                Case "User contributions" : GetList(New ContribsListRequest(_Source))
                Case "Watchlist" : GotList(Nothing, PageNames(Watchlist))
            End Select

            _Refreshing = True
            MainForm.DrawQueues()
        End If
    End Sub

    Private Sub GetList(ByVal Request As ListRequest)
        Request.Limit = _Limit
        Request.Spaces = _Spaces
        Request.TitleRegex = _PageRegex
        Request.Start(AddressOf GotList)
    End Sub

    Private Sub GotList(ByVal Result As RequestResult, ByVal ListItems As List(Of String))

        If Result IsNot Nothing AndAlso Result.Error Then
            Log(Msg("queue-refresh-fail", Name) & ": " & Result.ErrorMessage)
            Exit Sub
        End If

        If ListItems IsNot Nothing Then
            If _Pages Is Nothing Then _Pages = New List(Of String)
            _Pages.Clear()
            Items.Clear()

            For Each Item As String In ListItems
                If _RefreshReAdd OrElse Not _RemovedPages.Contains(Item) Then _Pages.Add(Item)

                Dim Page As Page = GetPage(Item)

                If Page.LastEdit IsNot Nothing Then
                    Items.Add(Page.LastEdit)
                Else
                    Dim NewEdit As New Edit
                    NewEdit.Page = Page
                    NewEdit.Id = "cur"
                    NewEdit.Oldid = "prev"
                    Items.Add(NewEdit)
                End If
            Next Item
        End If

        _Refreshing = False
        RefreshTimer.Start()
        MainForm.DrawQueues()
    End Sub

End Class

Enum DiffMode As Integer
    : None : Preload : All
End Enum

Enum QueueSortOrder As Integer
    : Time : TimeReverse : Quality
End Enum

Enum QueueType As Integer
    : FixedList : LiveList : Live : Dynamic
End Enum

Enum QueueFilter As Integer
    : Exclude : Require : None
End Enum