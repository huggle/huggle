<DebuggerDisplay("{Id}")> _
Class Edit

    'Represents a page revision

    Private Random As Double

    Public Shared All As New Dictionary(Of String, Edit)

    Public Sub New()
        'Random value to vary sort order
        Random = (New Random(Date.Now.Millisecond)).NextDouble
    End Sub

    Public Cached As CacheState
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
    Public RollbackUrl As String
    Public Size As Integer
    Public Summary As String
    Public Time As Date
    Public Type As Types
    Public TypeToWarn As String
    Public User As User
    Public WarningLevel As UserLevel

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

    Public ReadOnly Property Icon() As Image
        Get
            'Get an icon representing this edit
            Select Case Type
                Case Edit.Types.Blanked : Return My.Resources.blob_blanked
                Case Edit.Types.ReplacedWith : Return My.Resources.blob_replaced
                Case Edit.Types.Redirect : Return My.Resources.blob_redirect
                Case Edit.Types.Revert : Return My.Resources.blob_revert
                Case Edit.Types.Report : Return My.Resources.blob_report
                Case Edit.Types.Message : Return My.Resources.blob_message
                Case Edit.Types.Tag : Return My.Resources.blob_tag

                Case Edit.Types.Warning
                    Select Case WarningLevel
                        Case UserLevel.Warning, UserLevel.Warn1, UserLevel.Warn2, UserLevel.Warn3, _
                            UserLevel.Warn4im, UserLevel.WarnFinal : Return My.Resources.blob_warning
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
                            Select Case User.WarningLevel
                                Case UserLevel.Blocked : Return My.Resources.blob_blocked
                                Case UserLevel.ReportedAIV : Return My.Resources.blob_reported
                                Case UserLevel.Warn1 : Return My.Resources.blob_warn_1
                                Case UserLevel.Warn2 : Return My.Resources.blob_warn_2
                                Case UserLevel.Warn3 : Return My.Resources.blob_warn_3
                                Case UserLevel.WarnFinal : Return My.Resources.blob_warn_4
                                Case UserLevel.Reverted : Return My.Resources.blob_reverted
                            End Select
                        End If

                        If User.Anonymous Then Return My.Resources.blob_anon Else Return My.Resources.blob_none
                    End If
            End Select

            Return My.Resources.blob_none
        End Get
    End Property

    Public NotInheritable Class CompareByQuality : Implements IComparer(Of Edit)

        Public Function Compare(ByVal x As Edit, ByVal y As Edit) As Integer Implements IComparer(Of Edit).Compare
            If x.Type > y.Type AndAlso x.Type >= Types.ReplacedWith Then Return -1
            If y.Type > x.Type AndAlso y.Type >= Types.ReplacedWith Then Return 1

            If x.User.WarningLevel > y.User.WarningLevel AndAlso x.User.WarningLevel >= UserLevel.Reverted Then Return -1
            If y.User.WarningLevel > x.User.WarningLevel AndAlso y.User.WarningLevel >= UserLevel.Reverted Then Return 1

            If x.Page.Level > y.Page.Level Then Return -1
            If y.Page.Level > x.Page.Level Then Return 1

            If x.NewPage AndAlso Not y.NewPage Then Return -1
            If y.NewPage AndAlso Not x.NewPage Then Return 1

            If x.User.Anonymous AndAlso Not y.User.Anonymous Then Return -1
            If y.User.Anonymous AndAlso Not x.User.Anonymous Then Return 1

            If x.Page.Space.Number = 0 AndAlso y.Page.Space.Number > 0 Then Return -1
            If y.Page.Space.Number = 0 AndAlso x.Page.Space.Number > 0 Then Return 1

            If x.Random <> y.Random Then Return Math.Sign(y.Random - x.Random)

            Return String.Compare(x.Page.Name, y.Page.Name)
        End Function

    End Class

    Public NotInheritable Class CompareByPageName : Implements IComparer(Of Edit)

        Public Function Compare(ByVal x As Edit, ByVal y As Edit) As Integer Implements IComparer(Of Edit).Compare
            Return String.Compare(x.Page.Name, y.Page.Name)
        End Function

    End Class

    Public NotInheritable Class CompareByTime : Implements IComparer(Of Edit)

        Public Function Compare(ByVal x As Edit, ByVal y As Edit) As Integer Implements IComparer(Of Edit).Compare
            Return Date.Compare(x.Time, y.Time)
        End Function

    End Class

End Class
