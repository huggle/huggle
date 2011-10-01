<DebuggerDisplay("{Name}")> <DebuggerStepThrough()> _
Class Page

    'Represents a MediaWiki page

    Private _Name As String
    Private _Space As Space
    Private _Exists As Boolean

    Private Shared All As New Dictionary(Of String, Page)

    Public FirstEdit As Edit
    Public LastEdit As Edit
    Public Level As PageLevel
    Public Deletes As List(Of Delete)
    Public DeletesCurrent As Boolean
    Public Protections As List(Of Protection)
    Public ProtectionsCurrent As Boolean
    Public HistoryOffset As String
    Public Patrolled As Boolean
    Public Rcid As String
    Public SpeedyCriterion As SpeedyCriterion
    Public Text As String
    Public EditLevel As String
    Public MoveLevel As String

    Private Sub New(ByVal Name As String)
        _Name = Name
        _Space = Space.GetSpace(_Name)
        All.Add(_Name, Me)
    End Sub

    Public ReadOnly Property BaseName() As String
        Get
            If Space Is Space.Article OrElse Name.Length <= Space.Name.Length + 1 Then Return Name _
                Else Return Name.Substring(Space.Name.Length + 1)
        End Get
    End Property

    Public ReadOnly Property Creator() As User
        Get
            If FirstEdit Is Nothing Then Return Nothing
            Return FirstEdit.User
        End Get
    End Property

    Public ReadOnly Property Edits() As List(Of Edit)
        Get
            Dim PageEdits As New List(Of Edit)
            Dim Edit As Edit = LastEdit

            While Edit IsNot Nothing AndAlso Edit IsNot NullEdit
                PageEdits.Add(Edit)
                Edit = Edit.Prev
            End While

            Return PageEdits
        End Get
    End Property

    Public Property Exists() As Boolean
        Get
            Return _Exists
        End Get
        Set(ByVal value As Boolean)
            _Exists = value
        End Set
    End Property

    Public ReadOnly Property IsArticle() As Boolean
        Get
            Return (Space Is Space.Article)
        End Get
    End Property

    Public ReadOnly Property IsArticleTalkPage() As Boolean
        Get
            Return (Space Is Space.Talk)
        End Get
    End Property

    Public ReadOnly Property IsEditable() As Boolean
        Get
            Return Not ((EditLevel = "sysop" OrElse Space.Locked) AndAlso Not Config.Rights.Contains("protect"))
        End Get
    End Property

    Public ReadOnly Property IsMovable() As Boolean
        Get
            Return Not (Space.Unmovable OrElse ((MoveLevel = "sysop" OrElse Space.Locked) _
                AndAlso Not Config.Rights.Contains("protect")))
        End Get
    End Property

    Public ReadOnly Property IsSubjectPage() As Boolean
        Get
            Return Space.IsSubjectSpace
        End Get
    End Property

    Public ReadOnly Property IsSubpage() As Boolean
        Get
            Return (Space.Subpages AndAlso Name.Contains("/"))
        End Get
    End Property

    Public ReadOnly Property IsTalkPage() As Boolean
        Get
            Return Space.IsTalkSpace
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property

    Public ReadOnly Property Owner() As User
        Get
            If (Space Is Space.User OrElse Space Is Space.UserTalk) AndAlso Name.Contains(":") Then
                Dim Username As String = Name.Substring(Name.IndexOf(":"))
                If Username.Contains("/") Then Username = Username.Substring(0, Username.IndexOf("/"))
                Return GetUser(Username)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property Space() As Space
        Get
            Return _Space
        End Get
    End Property

    Public ReadOnly Property SubjectPage() As Page
        Get
            Return GetPage(SubjectPageName)
        End Get
    End Property

    Public ReadOnly Property SubjectPageName() As String
        Get
            If Space Is Space.Article Then Return BaseName _
                Else If Space.IsSubjectSpace Then Return Name _
                Else Return Space.SubjectSpace.Name & ":" & BaseName
        End Get
    End Property

    Public ReadOnly Property TalkPage() As Page
        Get
            Return GetPage(TalkPageName)
        End Get
    End Property

    Public ReadOnly Property TalkPageName() As String
        Get
            Return Space.TalkSpace.Name & ":" & BaseName
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return _Name
    End Function

    Public Sub MovedTo(ByVal NewName As String)
        'Handle a page move
        If All.ContainsKey(NewName) Then All.Remove(NewName)
        All.Remove(_Name)
        _Name = NewName
        All.Add(NewName, Me)
    End Sub

    Public Shared Sub ClearAll()
        All.Clear()
    End Sub

    Public Shared Function GetPage(ByVal Name As String) As Page
        Name = SanitizeTitle(Name)
        If Name Is Nothing Then Return Nothing
        If All.ContainsKey(Name) Then Return All(Name) Else Return New Page(Name)
    End Function

    Public Shared Function SanitizeTitle(ByVal Name As String) As String
        'Remove illegal characters
        If Name.Contains("#") Then Name = Name.Substring(0, Name.IndexOf("#"))

        Name = Name.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("|", "") _
            .Replace("<", "").Replace(">", "").Replace("#", "").Replace(Tab, "").Replace(LF, "") _
            .Replace("_", " ").Trim(" "c)

        While Name.StartsWith(":")
            Name = Name.Substring(1)
        End While

        If Name Is Nothing OrElse Name.Length = 0 Then Return Nothing

        'Capitalize
        If Name.Length > 1 Then Name = Name.Substring(0, 1).ToUpper & Name.Substring(1) Else Name = Name.ToUpper

        'Handle special namespaces
        Name = Name.Replace("Special:Mypage", Space.User.Name & ":" & Config.Username)
        Name = Name.Replace("Special:Mytalk", Space.UserTalk.Name & ":" & Config.Username)

        For Each Item As String In Space.Special
            If Name.StartsWith(Item & ":") Then Return Nothing
        Next Item

        'Handle namespace aliases
        For Each Item As KeyValuePair(Of String, Integer) In Space.Aliases
            If Name.StartsWith(Item.Key & ":") Then
                Name = Space.GetSpace(Item.Value).Name & Name.Substring(Name.IndexOf(":"))
                Exit For
            End If
        Next Item

        Return Name
    End Function

End Class

Public Enum PageLevel As Integer
    None = 0
    Ignore = -1
    Watch = 1
    Bad = 2
End Enum