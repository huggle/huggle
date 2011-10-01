NotInheritable Class Stats

    'Static class keeping track of various statistics

    Public Shared Groups As New Dictionary(Of String, Group)

    Public Class Group

        Public Shared ItemNames As String() = {"edits", "assisted", "huggle", "reverts", "warnings", "reports", _
            "tags", "notifications", "blocks", "deletes", "protections"}

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
        For Each Item As String In New String() {"allusers", "me", "ignored", "anon", "bots", "other"}
            Groups.Add(Item, New Group)
        Next Item
    End Sub

    Public Shared Sub Update(ByVal Item As Edit)
        'Update statistics and edit counts
        UpdateGroup("allusers", Item)

        If Item.User IsNot Nothing Then _
            If Item.User.IsMe Then UpdateGroup("me", Item) _
            Else If Item.User.Anonymous Then UpdateGroup("anon", Item) _
            Else If Item.Bot Then UpdateGroup("bots", Item) _
            Else If Item.User.Ignored Then UpdateGroup("ignored", Item) _
            Else UpdateGroup("other", Item)
    End Sub

    Public Shared Sub UpdateGroup(ByVal Name As String, ByVal Item As Edit)
        Dim Group As Group = Groups(Name)

        Group("edits") += 1

        If Item.Type = EditType.Notification Then Group("notifications") += 1
        If Item.Type = EditType.Report Then Group("reports") += 1
        If Item.Type = EditType.Revert Then Group("reverts") += 1
        If Item.Type = EditType.Tag Then Group("tags") += 1
        If Item.Type = EditType.Warning Then Group("warnings") += 1
        If Item.Assisted Then Group("assisted") += 1
        If Item.IsHuggleEdit Then Group("huggle") += 1
    End Sub

    Public Shared Sub Update(ByVal Item As Block)
        Groups("allusers")("blocks") += 1

        If Item.Admin IsNot Nothing Then _
            If Item.Admin.IsMe Then Groups("me")("blocks") += 1 Else Groups("ignored")("blocks") += 1
    End Sub

    Public Shared Sub Update(ByVal Item As Delete)
        Groups("allusers")("deletes") += 1

        If Item.Admin IsNot Nothing Then _
            If Item.Admin.IsMe Then Groups("me")("deletes") += 1 Else Groups("ignored")("deletes") += 1
    End Sub

    Public Shared Sub Update(ByVal Item As Protection)
        Groups("allusers")("protections") += 1

        If Item.Admin IsNot Nothing Then _
            If Item.Admin.IsMe Then Groups("me")("protections") += 1 Else Groups("ignored")("protections") += 1
    End Sub

End Class
