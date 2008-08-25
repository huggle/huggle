Imports System.IO
Imports System.Text.RegularExpressions

Class Queue

    'Represents a queue of revisions

    Public Shared All As New Dictionary(Of String, Queue)
    Public Shared DefaultQueue As Queue

    Private Items As SortedList(Of Edit, Boolean)

    Private _FilterAnonymous As QueueFilter = QueueFilter.None
    Private _FilterIgnored As QueueFilter = QueueFilter.None
    Private _FilterNewPage As QueueFilter = QueueFilter.None
    Private _FilterOwnUserspace As QueueFilter = QueueFilter.None
    Private _FilterReverts As QueueFilter = QueueFilter.None

    Private _Name As String
    Private _NeedsReset As Boolean
    Private _PageRegex As Regex
    Private _Pages As New List(Of String)
    Private _RemoveOld As Boolean
    Private _RemoveAfter As Integer
    Private _RemoveViewed As Boolean
    Private _SortOrder As QueueSortOrder
    Private _Spaces As New List(Of Space)
    Private _Type As QueueType
    Private _UserRegex As Regex

    Public Sub New(ByVal Name As String)
        _Name = Name
        _RemoveViewed = True
        _Spaces.AddRange(Space.All)
        All.Add(Name, Me)
    End Sub

    Public ReadOnly Property Edits() As IList(Of Edit)
        Get
            If NeedsReset Then Reset()
            Return Items.Keys
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

    Public Property FilterAnonymous() As QueueFilter
        Get
            Return _FilterAnonymous
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterAnonymous Then _NeedsReset = True
            _FilterAnonymous = value
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

    Public Property FilterNewPage() As QueueFilter
        Get
            Return _FilterNewPage
        End Get
        Set(ByVal value As QueueFilter)
            If value <> _FilterNewPage Then _NeedsReset = True
            _FilterNewPage = value
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
            If value.ToString <> _PageRegex.ToString Then _NeedsReset = True
            _PageRegex = value
        End Set
    End Property

    Public Property Pages() As List(Of String)
        Get
            Return _Pages
        End Get
        Set(ByVal value As List(Of String))
            _Pages = value
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

    Public Property Type() As QueueType
        Get
            Return _Type
        End Get
        Set(ByVal value As QueueType)
            If value <> _Type Then _NeedsReset = True
            _Type = value
        End Set
    End Property

    Public Property UserRegex() As Regex
        Get
            Return _UserRegex
        End Get
        Set(ByVal value As Regex)
            If value.ToString <> _UserRegex.ToString Then _NeedsReset = True
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

    Public Sub Reset()
        'Reset a queue - clear a live one, populate a fixed one

        'Define sort order
        Dim Comparer As IComparer(Of Edit)

        If Type = QueueType.FixedList Then
            Comparer = New Edit.CompareByPageName
        ElseIf SortOrder = QueueSortOrder.Quality Then
            Comparer = New Edit.CompareByQuality
        Else
            Comparer = New Edit.CompareByTime
        End If

        Dim QueueSize As Integer
        If Type = QueueType.FixedList Then QueueSize = Pages.Count Else QueueSize = Config.QueueSize

        Items = New SortedList(Of Edit, Boolean)(QueueSize, Comparer)

        'Populate a fixed queue
        If _Type = QueueType.FixedList Then
            For Each Item As String In _Pages
                Dim Page As Page = GetPage(Item)

                If Page.LastEdit IsNot Nothing Then
                    Items.Add(Page.LastEdit, False)
                Else
                    Dim NewEdit As New Edit
                    NewEdit.Page = Page
                    NewEdit.Id = "cur"
                    NewEdit.Oldid = "prev"
                    Items.Add(NewEdit, False)
                End If
            Next Item
        End If

        _NeedsReset = False
    End Sub

    Public Sub AddEdit(ByVal Edit As Edit)
        If Items IsNot Nothing Then
            If Type <> QueueType.FixedList Then RemoveOldEdits()

            'Add edit to the queue
            If Not Items.ContainsKey(Edit) Then
                Items.Add(Edit, False)
                If Items.Count > Config.QueueSize Then Items.RemoveAt(5000)
            End If
        End If
    End Sub

    Public Sub RemoveOldEdits()
        RemoveOldEdits(RemoveAfter)
    End Sub

    Public Sub RemoveOldEdits(ByVal Time As Integer)
        'Remove old edits and edits to the same page
        If RemoveOld OrElse Time > 0 Then
            Dim i As Integer = 0

            While i < Edits.Count
                If (RemoveOld AndAlso Edits(i) IsNot Edits(i).Page.LastEdit) _
                    OrElse (RemoveAfter > 0 AndAlso Edits(i).Time.AddMinutes(Time) < Date.UtcNow) Then

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
            If Items.ContainsKey(Edit) Then
                Items.Remove(Edit)
                If MatchesFilter(Edit) Then Items.Add(Edit, False)
            End If
        End If
    End Sub

    Public Sub RemoveViewedEdit(ByVal Edit As Edit)
        'Remove an edit that has been viewed, possibly on another queue
        If Items IsNot Nothing AndAlso RemoveViewed Then Items.Remove(Edit)
    End Sub

    Public Function MatchesFilter(ByVal PageName As String) As Boolean
        'Determines whether a page name matches this queue's filter
        Dim Page As Page = GetPage(PageName)

        If _PageRegex IsNot Nothing AndAlso Not _PageRegex.IsMatch(Page.Name) Then Return False

        Return True
    End Function

    Public Function MatchesFilter(ByVal Edit As Edit) As Boolean
        'Determines whether an edit matches this queue's filter
        If Edit.Deleted Then Return False

        If Type = QueueType.FixedList Then Return False
        If Type = QueueType.LiveList AndAlso Not Pages.Contains(Edit.Page.Name) Then Return False
        If _Spaces.Count > 0 AndAlso Not _Spaces.Contains(Edit.Page.Space) Then Return False
        If _PageRegex IsNot Nothing AndAlso Not _PageRegex.IsMatch(Edit.Page.Name) Then Return False
        If _UserRegex IsNot Nothing AndAlso Not _UserRegex.IsMatch(Edit.User.Name) Then Return False

        Return CSMatch(_FilterAnonymous, Edit.User.Anonymous) _
            AndAlso CSMatch(_FilterIgnored, Edit.User.Ignored) _
            AndAlso CSMatch(_FilterNewPage, Edit.NewPage) _
            AndAlso CSMatch(_FilterReverts, Edit.Type = Edit.Types.Revert)
    End Function

    Private Function CSMatch(ByVal Filter As QueueFilter, ByVal Value As Boolean) As Boolean
        'Helper function for above
        Select Case Filter
            Case QueueFilter.None : Return True
            Case QueueFilter.Require : Return Value
            Case QueueFilter.Exclude : Return Not Value
        End Select
    End Function

    Public Shared Sub LoadQueues()
        All.Clear()

        'Load queues from application data subfolder
        If Directory.Exists(QueuesLocation) Then
            For Each Path As String In Directory.GetFiles(QueuesLocation)
                Dim Items As New List(Of String)(File.ReadAllLines(Path))
                Dim Queue As Queue = Nothing

                For Each Item As String In Items
                    If Item.Contains(":") Then
                        Dim OptionName As String = Item.Substring(0, Item.IndexOf(":")), _
                            OptionValue As String = Item.Substring(Item.IndexOf(":") + 1)

                        Try
                            Select Case OptionName
                                Case "name" : Queue = New Queue(OptionValue)
                                Case "filter-anonymous" : Queue._FilterAnonymous = CType(OptionValue, QueueFilter)
                                Case "filter-ignored" : Queue._FilterIgnored = CType(OptionValue, QueueFilter)
                                Case "filter-new-pages" : Queue._FilterNewPage = CType(OptionValue, QueueFilter)
                                Case "filter-own-userspace" : Queue._FilterOwnUserspace = CType(OptionValue, QueueFilter)
                                Case "filter-reverts" : Queue._FilterReverts = CType(OptionValue, QueueFilter)
                                Case "page-regex" : Queue._PageRegex = New Regex(OptionValue)
                                Case "pages" : Queue._Pages.AddRange(OptionValue.Split("|"c))
                                Case "remove-after" : Queue._RemoveAfter = CInt(OptionValue)
                                Case "remove-old" : Queue._RemoveOld = CBool(OptionValue)
                                Case "remove-viewed" : Queue._RemoveViewed = CBool(OptionValue)
                                Case "sort-order" : Queue._SortOrder = CType(OptionValue, QueueSortOrder)
                                Case "spaces" : Queue._Spaces.AddRange(SetQueueSpaces(OptionValue))
                                Case "type" : Queue.Type = SetQueueType(OptionValue)
                                Case "user-regex" : Queue._UserRegex = New Regex(OptionValue)
                            End Select

                        Catch ex As Exception
                            'Ignore malformed config entries
                        End Try
                    End If
                Next Item

                Queue.Reset()
            Next Path
        End If

        If Queue.All.ContainsKey("Filtered edits") Then DefaultQueue = All("Filtered edits")
        SetDefaultQueues()
    End Sub

    Private Shared Sub SetDefaultQueues()
        'Create the default queues if they do not exist
        If Not Queue.All.ContainsKey("Filtered edits") Then
            DefaultQueue = New Queue("Filtered edits")
            DefaultQueue.Type = QueueType.Live
            DefaultQueue.SortOrder = QueueSortOrder.Quality
            DefaultQueue.Spaces = New List(Of Space)(New Space() {Space.Article})
            DefaultQueue.FilterIgnored = QueueFilter.Exclude
            DefaultQueue.FilterOwnUserspace = QueueFilter.Exclude
            DefaultQueue.FilterReverts = QueueFilter.Exclude
            DefaultQueue.RemoveOld = True
            DefaultQueue.Reset()
        End If

        If Not Queue.All.ContainsKey("New pages") Then
            Dim NewQueue As New Queue("New pages")
            NewQueue.Type = QueueType.Live
            NewQueue.SortOrder = QueueSortOrder.Time
            NewQueue.FilterNewPage = QueueFilter.Require
            NewQueue.Reset()
        End If

        If Not Queue.All.ContainsKey("All edits") Then
            Dim NewQueue As New Queue("All edits")
            NewQueue.Type = QueueType.Live
            NewQueue.SortOrder = QueueSortOrder.Time
            NewQueue.Reset()
        End If
    End Sub

    Private Shared Function SetQueueSpaces(ByVal Value As String) As List(Of Space)
        'Helper function for above
        Dim Spaces As New List(Of Space)

        For Each Item As String In Value.Split(","c)
            If Not Spaces.Contains(Space.GetSpace(CInt(Item))) Then Spaces.Add(Space.GetSpace(CInt(Item)))
        Next Item

        Return Spaces
    End Function

    Private Shared Function SetQueueType(ByVal Value As String) As QueueType
        'Helper function for above
        Select Case Value
            Case "FixedList" : Return QueueType.FixedList
            Case "LiveList" : Return QueueType.LiveList
            Case Else : Return QueueType.Live
        End Select
    End Function

    Public Shared Sub SaveQueues()
        'Create subfolder if it does not exist
        If Not Directory.Exists(QueuesLocation) AndAlso Not Directory.CreateDirectory(QueuesLocation).Exists Then
            MessageBox.Show("Unable to save edit queues; could not create sub-folder.", "Huggle", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        'Write queues to application data subfolder
        For Each Queue As Queue In All.Values
            Dim Items As New List(Of String)

            Items.Add("name:" & Queue.Name)
            Items.Add("type:" & Queue.Type.ToString)
            Items.Add("filter-anonymous:" & CStr(CInt(Queue.FilterAnonymous)))
            Items.Add("filter-ignored:" & CStr(CInt(Queue.FilterIgnored)))
            Items.Add("filter-new-pages:" & CStr(CInt(Queue.FilterNewPage)))
            Items.Add("filter-own-userspace:" & CStr(CInt(Queue.FilterOwnUserspace)))
            Items.Add("filter-reverts:" & CStr(CInt(Queue.FilterReverts)))
            Items.Add("remove-after:" & CStr(Queue.RemoveAfter))
            Items.Add("remove-old:" & CStr(Queue.RemoveOld))
            Items.Add("remove-viewed:" & CStr(Queue.RemoveViewed))
            Items.Add("sort-order:" & CStr(CInt(Queue.SortOrder)))

            If Queue.Spaces.Count > 0 Then
                Dim Spaces As New List(Of String)

                For Each Space As Space In Queue.Spaces
                    Spaces.Add(CStr(Space.Number))
                Next Space

                Items.Add("spaces:" & String.Join(",", Spaces.ToArray))
            End If

            If Queue.PageRegex IsNot Nothing Then Items.Add("page-regex:" & Queue.PageRegex.ToString)
            If Queue.UserRegex IsNot Nothing Then Items.Add("user-regex:" & Queue.UserRegex.ToString)
            If Queue.Pages.Count > 0 Then Items.Add("pages:" & String.Join("|", Queue.Pages.ToArray))

            File.WriteAllLines(QueuesLocation() & "\" & Queue.Name & ".txt", Items.ToArray)
        Next Queue
    End Sub

    Private Shared Function QueuesLocation() As String
        Return LocalConfigPath() & "\Queues\" & Config.Project
    End Function

End Class

Enum QueueSortOrder As Integer
    : Time : Quality
End Enum

Enum QueueType As Integer
    : FixedList : LiveList : Live
End Enum

Enum QueueFilter As Integer
    : Exclude : Require : None
End Enum