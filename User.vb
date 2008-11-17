Imports System.Text.RegularExpressions

<DebuggerDisplay("{Name}")> <DebuggerStepThrough()> _
Class User

    'Represents a user account or anonymous user

    'Regex to match IP addresses
    Private Shared AnonymousRegex As New Regex _
        ("((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)", RegexOptions.Compiled)

    Private Shared All As New Dictionary(Of String, User)

    Private _Anonymous As Boolean
    Private _EditCount As Integer
    Private _Ignored As Boolean
    Private _Level As UserLevel
    Private _Name As String
    Private _SessionEditCount As Integer
    Private _SharedIP As Boolean

    Public FirstEdit As Edit
    Public LastEdit As Edit
    Public Bot As Boolean
    Public WarnTime As Date
    Public Warnings As List(Of Warning)
    Public WarningsCurrent As Boolean
    Public Blocks As List(Of Block)
    Public BlocksCurrent As Boolean
    Public ContribsOffset As String
    Public RecentContribsRetrieved As Boolean

    Public Sub New(ByVal Name As String)
        _Name = Name
        _EditCount = -1
        _Anonymous = AnonymousRegex.IsMatch(Name)
        _Ignored = Whitelist.Contains(Name)
        If Not All.ContainsKey(Name) Then All.Add(Name, Me)
    End Sub

    Public Shared ReadOnly Property [Me]() As User
        Get
            Return GetUser(Config.Username)
        End Get
    End Property

    Public ReadOnly Property Anonymous() As Boolean
        Get
            Return _Anonymous
        End Get
    End Property

    Public Property EditCount() As Integer
        Get
            Return _EditCount
        End Get
        Set(ByVal value As Integer)
            _EditCount = value
            RefreshInfo()
        End Set
    End Property

    Public ReadOnly Property Edits() As List(Of Edit)
        Get
            Dim UserEdits As New List(Of Edit)
            Dim Edit As Edit = LastEdit

            While Edit IsNot Nothing AndAlso Edit IsNot NullEdit
                UserEdits.Add(Edit)
                Edit = Edit.PrevByUser
            End While

            Return UserEdits
        End Get
    End Property

    Public ReadOnly Property Range() As String
        Get
            If Not Anonymous Then Return Nothing

            Dim NamePart As String = Name.Substring(0, Name.LastIndexOf("."))
            NamePart = NamePart.Substring(0, NamePart.LastIndexOf("."))
            Return NamePart
        End Get
    End Property

    Public Property Ignored() As Boolean
        Get
            Return _Ignored
        End Get
        Set(ByVal value As Boolean)
            _Ignored = value
            RefreshEdits()
            RefreshInfo()
        End Set
    End Property

    Public ReadOnly Property IsMe() As Boolean
        Get
            Return (Name = Config.Username)
        End Get
    End Property

    Public Property Level() As UserLevel
        Get
            Return _Level
        End Get
        Set(ByVal value As UserLevel)
            _Level = value
            RefreshEdits()
            RefreshInfo()
        End Set
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property

    Public Property SessionEditCount() As Integer
        Get
            Return _SessionEditCount
        End Get
        Set(ByVal value As Integer)
            _SessionEditCount = value
            RefreshInfo()
        End Set
    End Property

    Public Property SharedIP() As Boolean
        Get
            Return _SharedIP
        End Get
        Set(ByVal value As Boolean)
            _SharedIP = value
            RefreshInfo()
        End Set
    End Property

    Public ReadOnly Property TalkPage() As Page
        Get
            Return GetPage(Space.UserTalk.Name & ":" & Name)
        End Get
    End Property

    Public ReadOnly Property Userpage() As Page
        Get
            Return GetPage(Space.User.Name & ":" & Name)
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return _Name
    End Function

    Public Shared Sub ClearAll()
        All.Clear()
    End Sub

    Public Sub RefreshEdits()
        'Remove and re-add edits with changed properties that might affect sort order
        For Each Item As Edit In Edits
            For Each Queue As Queue In Queue.All.Values
                Queue.RefreshEdit(Item)
            Next Queue
        Next Item
    End Sub

    Public Sub RefreshInfo()
        'Refresh any open user info forms
        For Each Item As Form In Application.OpenForms
            Dim Form As UserInfoForm = TryCast(Item, UserInfoForm)
            If Form IsNot Nothing AndAlso Form.User Is Me Then Form.RefreshData()
        Next Item
    End Sub

    Public Shared Function GetUser(ByVal Name As String) As User
        Name = SanitizeName(Name)
        If Name Is Nothing Then Return Nothing
        If All.ContainsKey(Name) Then Return All(Name) Else Return New User(Name)
    End Function

    Public Shared Function SanitizeName(ByVal Name As String) As String
        'Remove illegal characters
        If Name.Contains("#") Then Name = Name.Substring(0, Name.IndexOf("#"))

        Name = Name.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("|", "") _
            .Replace("<", "").Replace(">", "").Replace("#", "").Replace(Tab, "").Replace(LF, "") _
            .Replace("_", " ").Trim(" "c)

        If Name.EndsWith(":") Then Return Nothing
        Name = Name.Substring(Name.LastIndexOf(":") + 1)
        
        If Name Is Nothing OrElse Name.Length = 0 Then Return Nothing

        'Capitalize
        If Name.Length > 1 Then Name = Name.Substring(0, 1).ToUpper & Name.Substring(1) Else Name = Name.ToUpper

        Return Name
    End Function

End Class

Enum UserLevel As Integer
    None
    Notification
    Reverted
    ReportedUAA
    Message
    Warning
    Warn1
    Warn2
    Warn3
    Warn4im
    WarnFinal
    ReportedAIV
    Blocked
End Enum