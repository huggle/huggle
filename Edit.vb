<DebuggerDisplay("{Id}")> _
Class Edit

    'Represents a page revision

    Private Random As Double

    Public Sub New()
        'Random value to vary sort order
        Random = (New Random(Date.Now.Millisecond)).NextDouble
    End Sub

    Public Added As Boolean
    Public Cached As CacheState
    Public Deleted As Boolean
    Public Diff As String
    Public Id As String
    Public LevelToWarn As UserL
    Public Multiple As Boolean
    Public NewPage As Boolean
    Public [Next] As Edit
    Public NextByUser As Edit
    Public Oldid As String
    Public Page As Page
    Public Prev As Edit
    Public PrevByUser As Edit
    Public Processed As Boolean
    Public RollbackUrl As String
    Public Size As Integer
    Public Summary As String
    Public Time As Date
    Public Type As Types
    Public TypeToWarn As String
    Public User As User
    Public WarningLevel As UserL

    Public Enum Types As Integer
        Blanked = 2
        ReplacedWith = 1
        None = 0
        Revert = -1
        Message = -2
        Tag = -3
        Warning = -4
        Report = -5
        Redirect = -6
    End Enum

    Public Enum CacheState As Integer
        Uncached
        Caching
        Cached
        Viewed
    End Enum

    Public Shared Function Compare(ByVal X As Edit, ByVal Y As Edit) As Integer
        If X.Type > Y.Type AndAlso X.Type >= Types.ReplacedWith Then Return -1
        If Y.Type > X.Type AndAlso Y.Type >= Types.ReplacedWith Then Return 1

        If X.User.Level > Y.User.Level AndAlso X.User.Level >= UserL.Reverted Then Return -1
        If Y.User.Level > X.User.Level AndAlso Y.User.Level >= UserL.Reverted Then Return 1

        If X.Page.Level > Y.Page.Level Then Return -1
        If Y.Page.Level > X.Page.Level Then Return 1

        If X.NewPage AndAlso Not Y.NewPage Then Return -1
        If Y.NewPage AndAlso Not X.NewPage Then Return 1

        If X.User.Anonymous AndAlso Not Y.User.Anonymous Then Return -1
        If Y.User.Anonymous AndAlso Not X.User.Anonymous Then Return 1

        If X.Page.Space.Number = 0 AndAlso Y.Page.Space.Number > 0 Then Return -1
        If Y.Page.Space.Number = 0 AndAlso X.Page.Space.Number > 0 Then Return 1

        If X.Random <> Y.Random Then Return Math.Sign(Y.Random - X.Random)

        Return String.Compare(X.Page.Name, Y.Page.Name)
    End Function

End Class
