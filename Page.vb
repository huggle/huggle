<DebuggerDisplay("{Name}")> _
Class Page

    'Represents a wiki page

    Public Name As String
    Public FirstEdit As Edit
    Public LastEdit As Edit
    Public Level As Levels
    Public [Namespace] As String
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

    Public ReadOnly Property IsMovable() As Boolean
        Get
            If ArrayContains(Config.UnmovableNamespaces, [Namespace]) Then Return False
            If MoveLevel = "sysop" AndAlso Not Administrator Then Return False
            If ArrayContains(Config.ProtectedNamespaces, [Namespace]) AndAlso Not Administrator Then Return False

            Return True
        End Get
    End Property

End Class
