<DebuggerDisplay("{Name}")> _
Class Space

    'Represents a MediaWiki namespace

    Private _Number As Integer, _Name As String, _Locked, _Unmovable, _Subpages As Boolean
    Private Shared _All As New List(Of Space)

    'Define built-in namespaces; extra ones are set in project configuration
    Public Shared ReadOnly _
        Article As New Space(0), _
        Talk As New Space(1, Subpages:=True), _
        User As New Space(2, Subpages:=True), _
        UserTalk As New Space(3, Subpages:=True), _
        Project As New Space(4, Subpages:=True), _
        ProjectTalk As New Space(5, Subpages:=True), _
        Image As New Space(6, Unmovable:=True), _
        ImageTalk As New Space(7, Subpages:=True), _
        MediaWiki As New Space(8, Locked:=True), _
        MediaWikiTalk As New Space(9, Subpages:=True), _
        Template As New Space(10, Subpages:=True), _
        TemplateTalk As New Space(11, Subpages:=True), _
        Help As New Space(12), _
        HelpTalk As New Space(13), _
        Category As New Space(14, Unmovable:=True), _
        CategoryTalk As New Space(15)

    Public Shared Aliases As New Dictionary(Of String, Integer)
    Public Shared Special As New List(Of String)

    Shared Sub New()
        Aliases.Add("Media", 6)
        Special.Add("Special")
    End Sub

    Private Sub New(ByVal Number As Integer, Optional ByVal Name As String = Nothing, _
        Optional ByVal Locked As Boolean = False, Optional ByVal Unmovable As Boolean = False, _
        Optional ByVal Subpages As Boolean = False)

        _Number = Number
        _Name = Name
        _Locked = Locked
        _Unmovable = Unmovable
        _Subpages = Subpages
        _All.Add(Me)
    End Sub

    Public Overrides Function ToString() As String
        Return _Name
    End Function

    Public Shared Sub SetName(ByVal Number As Integer, ByVal Name As String)
        For Each Item As Space In _All
            If Item.Number = Number Then
                Item._Name = Name
                Exit Sub
            End If
        Next Item

        Dim NewSpace As New Space(Number, Name, Subpages:=True)
    End Sub

    Public Shared ReadOnly Property All() As List(Of Space)
        Get
            Return _All
        End Get
    End Property

    Public Shared ReadOnly Property GetSpace(ByVal PageName As String) As Space
        Get
            If Not PageName.Contains(":") Then Return Article

            For Each Item As Space In _All
                If PageName.StartsWith(Item._Name & ":") Then Return Item
            Next Item

            Return Article
        End Get
    End Property

    Public Shared ReadOnly Property GetSpace(ByVal Number As Integer) As Space
        Get
            For Each Item As Space In _All
                If Item.Number = Number Then Return Item
            Next Item

            Return Nothing
        End Get
    End Property

    Public ReadOnly Property Number() As Integer
        Get
            Return _Number
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property

    Public ReadOnly Property Locked() As Boolean
        Get
            Return _Locked
        End Get
    End Property

    Public ReadOnly Property Unmovable() As Boolean
        Get
            Return _Unmovable
        End Get
    End Property

    Public ReadOnly Property Subpages() As Boolean
        Get
            Return _Subpages
        End Get
    End Property

    Public ReadOnly Property IsArticleSpace() As Boolean
        Get
            Return (Article Is Me)
        End Get
    End Property

    Public ReadOnly Property IsTalkSpace() As Boolean
        Get
            Return (Number Mod 2 = 1)
        End Get
    End Property

    Public ReadOnly Property SubjectSpace() As Space
        Get
            If IsTalkSpace Then Return GetSpace(Number - 1) Else Return Me
        End Get
    End Property

    Public ReadOnly Property TalkSpace() As Space
        Get
            If IsTalkSpace Then Return Me Else Return GetSpace(Number + 1)
        End Get
    End Property

End Class