<DebuggerDisplay("{Name}")> <DebuggerStepThrough()> _
Class Space

    'Represents a MediaWiki namespace

    Private _Number As Integer, _Name As String, _Locked, _Unmovable, _Subpages As Boolean
    Private Shared _All As New List(Of Space)

    'Define built-in namespaces; extra ones are set in project configuration
    Public Shared ReadOnly _
        Article As New Space(0), _
        Talk As New Space(1, "Talk", Subpages:=True), _
        User As New Space(2, "User", Subpages:=True), _
        UserTalk As New Space(3, "User talk", Subpages:=True), _
        Project As New Space(4, "Project", Subpages:=True), _
        ProjectTalk As New Space(5, "Project talk", Subpages:=True), _
        Image As New Space(6, "Image", Unmovable:=True), _
        ImageTalk As New Space(7, "Image talk", Subpages:=True), _
        MediaWiki As New Space(8, "MediaWiki", Locked:=True), _
        MediaWikiTalk As New Space(9, "MediaWiki talk", Subpages:=True), _
        Template As New Space(10, "Template", Subpages:=True), _
        TemplateTalk As New Space(11, "Template talk", Subpages:=True), _
        Help As New Space(12, "Help"), _
        HelpTalk As New Space(13, "Help talk"), _
        Category As New Space(14, "Category", Unmovable:=True), _
        CategoryTalk As New Space(15, "Category talk")

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

    Public Shared Operator =(ByVal a As Space, ByVal b As Space) As Boolean
        Return (a Is b)
    End Operator

    Public Shared Operator <>(ByVal a As Space, ByVal b As Space) As Boolean
        Return (a IsNot b)
    End Operator

    Public ReadOnly Property IsSubjectSpace() As Boolean
        Get
            Return (Number Mod 2 = 0)
        End Get
    End Property

    Public ReadOnly Property IsTalkSpace() As Boolean
        Get
            Return (Number Mod 2 = 1)
        End Get
    End Property

    Public ReadOnly Property Locked() As Boolean
        Get
            Return _Locked
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property

    Public ReadOnly Property Number() As Integer
        Get
            Return _Number
        End Get
    End Property

    Public ReadOnly Property SubjectSpace() As Space
        Get
            If IsTalkSpace Then Return GetSpace(Number - 1) Else Return Me
        End Get
    End Property

    Public ReadOnly Property Subpages() As Boolean
        Get
            Return _Subpages
        End Get
    End Property

    Public ReadOnly Property TalkSpace() As Space
        Get
            If IsTalkSpace Then Return Me Else Return GetSpace(Number + 1)
        End Get
    End Property

    Public ReadOnly Property Unmovable() As Boolean
        Get
            Return _Unmovable
        End Get
    End Property

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

End Class