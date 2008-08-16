<DebuggerDisplay("{Name}")> _
Class Page

    'Represents a MediaWiki page

    Private _Name As String

    Private Shared All As New Dictionary(Of String, Page)

    Public FirstEdit As Edit
    Public LastEdit As Edit
    Public Level As Levels
    Public Deletes As List(Of Delete)
    Public DeletesCurrent As Boolean
    Public Protections As List(Of Protection)
    Public ProtectionsCurrent As Boolean
    Public HistoryOffset As String
    Public Patrolled As Boolean
    Public Rcid As String
    Public EditLevel As String
    Public MoveLevel As String

    Public Enum Levels As Integer
        None = 0
        Ignore = -1
        Watch = 1
        Bad = 2
    End Enum

    Private Sub New(ByVal Name As String)
        _Name = Name
        All.Add(_Name, Me)
    End Sub

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

    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property

    Public ReadOnly Property Space() As Space
        Get
            Return Space.GetSpace(_Name)
        End Get
    End Property

    Public ReadOnly Property IsEditable() As Boolean
        Get
            If (EditLevel = "sysop" OrElse Space.Locked) AndAlso Not Administrator Then Return False
            Return True
        End Get
    End Property

    Public ReadOnly Property IsMovable() As Boolean
        Get
            If Space.Unmovable Then Return False
            If (MoveLevel = "sysop" OrElse Space.Locked) AndAlso Not Administrator Then Return False
            Return True
        End Get
    End Property

    Public ReadOnly Property IsSubpage() As Boolean
        Get
            Return (Space.Subpages AndAlso Name.Contains("/"))
        End Get
    End Property

    Public ReadOnly Property BasePageName() As String
        Get
            If Space.IsArticleSpace Then Return Name Else Return Name.Substring(Space.Name.Length + 1)
        End Get
    End Property

    Public ReadOnly Property IsArticle() As Boolean
        Get
            Return Space.IsArticleSpace
        End Get
    End Property

    Public ReadOnly Property IsArticleTalkPage() As Boolean
        Get
            Return Space.SubjectSpace.IsArticleSpace
        End Get
    End Property

    Public ReadOnly Property IsTalkPage() As Boolean
        Get
            Return Space.IsTalkSpace
        End Get
    End Property

    Public ReadOnly Property TalkPageName() As String
        Get
            Return Space.TalkSpace.Name & ":" & BasePageName
        End Get
    End Property

    Public ReadOnly Property TalkPage() As Page
        Get
            Return GetPage(TalkPageName)
        End Get
    End Property

    Public ReadOnly Property SubjectPageName() As String
        Get
            If Space.IsArticleSpace Then Return BasePageName Else Return Space.SubjectSpace.Name & ":" & BasePageName
        End Get
    End Property

    Public ReadOnly Property SubjectPage() As Page
        Get
            Return GetPage(SubjectPageName)
        End Get
    End Property

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
        Name = Name.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("|", "") _
            .Replace("<", "").Replace(">", "").Replace("#", "").Replace(CChar(vbTab), "").Replace(CChar(vbLf), "") _
            .Replace(CChar(vbCr), "").Replace("_", " ").Trim(" "c)
        If Name.Contains("#") Then Name = Name.Substring(0, Name.IndexOf("#"))
        If Name Is Nothing OrElse Name.Length = 0 Then Return Nothing

        'Capitalize
        If Name.Length > 1 Then Name = Name.Substring(0, 1).ToUpper & Name.Substring(1) Else Name = Name.ToUpper

        'Handle special namespaces
        Name = Name.Replace("Special:Mypage", Space.User.Name & ":" & Username)
        Name = Name.Replace("Special:Mytalk", Space.UserTalk.Name & ":" & Username)

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

    Public Sub MovedTo(ByVal NewName As String)
        'Handle a page move
        If All.ContainsKey(NewName) Then All.Remove(NewName)
        All.Remove(_Name)
        _Name = NewName
        All.Add(NewName, Me)
    End Sub

End Class