NotInheritable Class Stats

    'Static class keeping track of various statistics

    Public Shared Groups As New Dictionary(Of String, Group)

    Public Class Group

        Public Shared ItemNames As String() = {Msg("stats-edits"), Msg("stats-assisted"), Msg("stats-huggle"), _
            Msg("stats-reverts"), Msg("stats-warnings"), Msg("stats-reports"), Msg("stats-tags"), _
            Msg("stats-notifications"), Msg("stats-blocks"), Msg("stats-deletes"), Msg("stats-protections")}

        Public Items As New Dictionary(Of String, Integer)

        Public Sub New()
            For Each Item As String In ItemNames
                Items.Add(Item, 0)
            Next Item
        End Sub

        Default Public Property Item(ByVal Name As String) As Integer
            Get
                Return Items(Name)
            End Get
            Set(ByVal value As Integer)
                Items(Name) = value
            End Set
        End Property

    End Class

    Shared Sub New()
        For Each Item As String In New String() {Msg("stats-allusers"), Msg("stats-me"), Msg("stats-ignored"), _
            Msg("stats-anon"), Msg("stats-bots"), Msg("stats-other")}
            Groups.Add(Item, New Group)
        Next Item
    End Sub

    Public Shared Sub Update(ByVal Item As Edit)
        'Update statistics and edit counts
        UpdateGroup(Msg("stats-allusers"), Item)

        If Item.User IsNot Nothing Then _
            If Item.User.IsMe Then UpdateGroup(Msg("stats-me"), Item) _
            Else If Item.User.Anonymous Then UpdateGroup(Msg("stats-anon"), Item) _
            Else If Item.User.Bot Then UpdateGroup(Msg("stats-bots"), Item) _
            Else If Item.User.Ignored Then UpdateGroup(Msg("stats-ignored"), Item) _
            Else UpdateGroup(Msg("stats-other"), Item)
    End Sub

    Public Shared Sub UpdateGroup(ByVal Name As String, ByVal Item As Edit)
        Dim Group As Group = Groups(Name)

        Group(Msg("stats-edits")) += 1

        If Item.Type = EditType.Notification Then Group(Msg("stats-notifications")) += 1
        If Item.Type = EditType.Report Then Group(Msg("stats-reports")) += 1
        If Item.Type = EditType.Revert Then Group(Msg("stats-reverts")) += 1
        If Item.Type = EditType.Tag Then Group(Msg("stats-tags")) += 1
        If Item.Type = EditType.Warning Then Group(Msg("stats-warnings")) += 1
        If Item.Assisted Then Group(Msg("stats-assisted")) += 1
        If Item.IsHuggleEdit Then Group(Msg("stats-huggle")) += 1
    End Sub

    Public Shared Sub Update(ByVal Item As Block)
        Groups(Msg("stats-allusers"))(Msg("stats-blocks")) += 1

        If Item.Admin IsNot Nothing Then _
            If Item.Admin.IsMe Then Groups(Msg("stats-me"))(Msg("stats-blocks")) += 1 _
            Else Groups(Msg("stats-ignored"))(Msg("stats-blocks")) += 1
    End Sub

    Public Shared Sub Update(ByVal Item As Delete)
        Groups(Msg("stats-allusers"))(Msg("stats-deletes")) += 1

        If Item.Admin IsNot Nothing Then _
            If Item.Admin.IsMe Then Groups(Msg("stats-me"))(Msg("stats-deletes")) += 1 _
            Else Groups(Msg("stats-ignored"))(Msg("stats-deletes")) += 1
    End Sub

    Public Shared Sub Update(ByVal Item As Protection)
        Groups(Msg("stats-allusers"))(Msg("stats-protections")) += 1

        If Item.Admin IsNot Nothing Then _
            If Item.Admin.IsMe Then Groups(Msg("stats-me"))(Msg("stats-protections")) += 1 _
            Else Groups(Msg("stats-ignored"))(Msg("stats-protections")) += 1
    End Sub

End Class
