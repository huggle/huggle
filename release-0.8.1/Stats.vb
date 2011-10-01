NotInheritable Class Stats

    'Static class keeping track of various statistics

    Public Shared Groups As New Dictionary(Of String, Group)

    Public Class Group

        Public Shared ItemNames As String() = {"Edits", "Assisted edits", "Huggle edits", "Reverts", _
            "Warnings", "Reports", "Tags", "Notifications", "Blocks", "Deletes", "Protections"}

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
        For Each Item As String In New String() {"All users", "Me", "Ignored", "Anonymous", "Bots", "Other"}
            Groups.Add(Item, New Group)
        Next Item
    End Sub

    Public Shared Sub Update(ByVal Item As Edit)
        'Update statistics and edit counts
        UpdateGroup("All users", Item)

        If Item.User IsNot Nothing Then _
            If Item.User.IsMe Then UpdateGroup("Me", Item) _
            Else If Item.User.Anonymous Then UpdateGroup("Anonymous", Item) _
            Else If Item.User.Bot Then UpdateGroup("Bots", Item) _
            Else If Item.User.Ignored Then UpdateGroup("Ignored", Item) _
            Else UpdateGroup("Other", Item)
    End Sub

    Public Shared Sub UpdateGroup(ByVal Name As String, ByVal Item As Edit)
        Dim Group As Group = Groups(Name)

        Group("Edits") += 1

        If Item.Type = EditType.Notification Then Group("Notifications") += 1
        If Item.Type = EditType.Report Then Group("Reports") += 1
        If Item.Type = EditType.Revert Then Group("Reverts") += 1
        If Item.Type = EditType.Tag Then Group("Tags") += 1
        If Item.Type = EditType.Warning Then Group("Warnings") += 1
        If Item.Assisted Then Group("Assisted edits") += 1
        If Item.IsHuggleEdit Then Group("Huggle edits") += 1
    End Sub

    Public Shared Sub Update(ByVal Item As Block)
        Groups("All users")("Blocks") += 1

        If Item.Admin IsNot Nothing Then _
            If Item.Admin.IsMe Then Groups("Me")("Blocks") += 1 _
            Else Groups("Ignored")("Blocks") += 1
    End Sub

    Public Shared Sub Update(ByVal Item As Delete)
        Groups("All users")("Deletes") += 1

        If Item.Admin IsNot Nothing Then _
            If Item.Admin.IsMe Then Groups("Me")("Deletes") += 1 _
            Else Groups("Ignored")("Deletes") += 1
    End Sub

    Public Shared Sub Update(ByVal Item As Protection)
        Groups("All users")("Protections") += 1

        If Item.Admin IsNot Nothing Then _
            If Item.Admin.IsMe Then Groups("Me")("Protections") += 1 _
            Else Groups("Ignored")("Protections") += 1
    End Sub

End Class
