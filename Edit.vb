<DebuggerDisplay("{Id}")> _
Class Edit

    'Represents a page revision

    Private Random As Double

    Public Shared All As New Dictionary(Of String, Edit)

    Public Sub New()
        'Random value to vary sort order
        Random = (New Random(Date.Now.Millisecond)).NextDouble
        Size = -1
    End Sub

    Public Assisted As Boolean
    Public Cached As CacheState
    Public Change As Integer
    Public Deleted As Boolean
    Public Diff As String
    Public Id As String
    Public LevelToWarn As UserLevel
    Public Multiple As Boolean
    Public NewPage As Boolean
    Public [Next] As Edit
    Public NextByUser As Edit
    Public Oldid As String
    Public Page As Page
    Public Prev As Edit
    Public PrevByUser As Edit
    Public Processed As Boolean
    Public Rcid As String
    Public RollbackToken As String
    Public Size As Integer
    Public Summary As String
    Public Text As String
    Public Time As Date
    Public Type As EditType
    Public TypeToWarn As String
    Public User As User
    Public WarningLevel As UserLevel

    Public Enum CacheState As Integer
        Uncached
        Caching
        Cached
        Viewed
    End Enum

    Public ReadOnly Property Icon() As Image
        Get
            'Get an icon representing this edit
            Select Case Type
                Case EditType.Blanked : Return My.Resources.blob_blanked
                Case EditType.ReplacedWith : Return My.Resources.blob_replaced
                Case EditType.Redirect : Return My.Resources.blob_redirect
                Case EditType.Revert : Return My.Resources.blob_revert
                Case EditType.Report : Return My.Resources.blob_report
                Case EditType.Notification : Return My.Resources.blob_message
                Case EditType.Tag : Return My.Resources.blob_tag

                    'Get the correct warning icon for each level
                Case EditType.Warning
                    Select Case WarningLevel
                        Case UserLevel.Warn1, UserLevel.Warn2, UserLevel.Warn3, _
                            UserLevel.Warn4im, UserLevel.WarnFinal : Return My.Resources.blob_blank
                        Case UserLevel.Warning : Return My.Resources.blob_warning
                        Case UserLevel.Blocked : Return My.Resources.blob_blocknote
                    End Select

                Case Else
                    If User IsNot Nothing Then
                        If User.IsMe Then
                            Return My.Resources.blob_me

                        ElseIf User.Bot Then
                            Return My.Resources.blob_bot

                        ElseIf User.Ignored Then
                            Return My.Resources.blob_ignored

                        Else
                            Select Case User.Level
                                Case UserLevel.Blocked : Return My.Resources.blob_blocked
                                Case UserLevel.ReportedAIV : Return My.Resources.blob_reported
                                Case UserLevel.Reverted : Return My.Resources.blob_reverted
                                Case UserLevel.Warn1 : Return My.Resources.blob_warn_1
                                Case UserLevel.Warn2 : Return My.Resources.blob_warn_2
                                Case UserLevel.Warn3 : Return My.Resources.blob_warn_3
                                Case UserLevel.WarnFinal : Return My.Resources.blob_warn_4
                            End Select
                        End If

                        If User.Anonymous Then Return My.Resources.blob_anon Else Return My.Resources.blob_none
                    End If
            End Select

            Return My.Resources.blob_none
        End Get
    End Property

    Public ReadOnly Property IsHuggleEdit() As Boolean
        Get
            If Config.Summary Is Nothing Then Return False _
                Else Return (Summary.Contains(Config.Summary) AndAlso User.Ignored)
        End Get
    End Property

    Public ReadOnly Property OwnUserspace() As Boolean
        Get
            Return (Page.Owner Is User)
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Id
    End Function

    Public NotInheritable Class CompareByQuality : Implements IComparer(Of Edit)

        <DebuggerStepThrough()> _
        Public Function Compare(ByVal x As Edit, ByVal y As Edit) As Integer Implements IComparer(Of Edit).Compare
            If x.User.Ignored AndAlso Not y.User.Ignored Then Return 1
            If y.User.Ignored AndAlso Not x.User.Ignored Then Return -1

            If x.User.Bot AndAlso Not y.User.Bot Then Return 1
            If y.User.Bot AndAlso Not x.User.Bot Then Return -1

            If x.Type > y.Type AndAlso x.Type >= EditType.ReplacedWith Then Return -1
            If y.Type > x.Type AndAlso y.Type >= EditType.ReplacedWith Then Return 1

            If x.User.Level > y.User.Level AndAlso x.User.Level >= UserLevel.Reverted Then Return -1
            If y.User.Level > x.User.Level AndAlso y.User.Level >= UserLevel.Reverted Then Return 1

            If x.Page Is Nothing Then Return 1
            If y.Page Is Nothing Then Return -1

            If x.Page.Level > y.Page.Level Then Return -1
            If y.Page.Level > x.Page.Level Then Return 1

            If x.User.Anonymous AndAlso Not y.User.Anonymous Then Return -1
            If y.User.Anonymous AndAlso Not x.User.Anonymous Then Return 1

            If x.Page.Space.Number = 0 AndAlso y.Page.Space.Number > 0 Then Return -1
            If y.Page.Space.Number = 0 AndAlso x.Page.Space.Number > 0 Then Return 1

            If x.Random <> y.Random Then Return Math.Sign(y.Random - x.Random)

            Return String.Compare(x.Page.Name, y.Page.Name)
        End Function

    End Class

    Public NotInheritable Class CompareByPageName : Implements IComparer(Of Edit)

        <DebuggerStepThrough()> _
        Public Function Compare(ByVal x As Edit, ByVal y As Edit) As Integer Implements IComparer(Of Edit).Compare
            Return String.Compare(x.Page.Name, y.Page.Name)
        End Function

    End Class

    Public NotInheritable Class CompareByTime : Implements IComparer(Of Edit)

        <DebuggerStepThrough()> _
        Public Function Compare(ByVal x As Edit, ByVal y As Edit) As Integer Implements IComparer(Of Edit).Compare
            Return Date.Compare(y.Time, x.Time)
        End Function

    End Class

    Public NotInheritable Class CompareByTimeReverse : Implements IComparer(Of Edit)

        <DebuggerStepThrough()> _
        Public Function Compare(ByVal x As Edit, ByVal y As Edit) As Integer Implements IComparer(Of Edit).Compare
            Return Date.Compare(x.Time, y.Time)
        End Function

    End Class

End Class

Public Enum EditType As Integer
    Blanked = 2
    ReplacedWith = 1
    None = 0
    Revert = -1
    Notification = -2
    Tag = -3
    Warning = -4
    Report = -5
    Redirect = -6
End Enum