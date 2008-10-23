Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class EditRequest : Inherits Request

        'Make an arbitrary edit

        Public Page As Page, Text, Summary As String, Minor, Watch, NoAutoSummary As Boolean

        Protected Overrides Sub Process()
            LogProgress(Msg("edit-progress", Page.Name))

            Dim Result As ApiResult = PostEdit(Page, Text, Summary, _
                Minor:=Minor, Watch:=Watch, SuppressAutoSummary:=NoAutoSummary)

            If Result.Error Then Fail(Msg("edit-fail", Page.Name), Result.ErrorMessage) Else Complete()
        End Sub

        Protected Overrides Sub Done()
            If State = States.Cancelled Then UndoEdit(Page)
        End Sub

    End Class

    Class TagRequest : Inherits Request

        'Tag a page

        Public Page As Page, NotifyRequest As UserMessageRequest
        Public Tag, Summary, AvoidText As String
        Public ReplacePage, Patrol, InsertAtEnd As Boolean

        Protected Overrides Sub Process()
            LogProgress(Msg("tag-progress", Page.Name))

            Dim Result As ApiResult = GetText(Page)

            If Result.Error Then
                Fail(Msg("tag-fail", Page.Name), Result.ErrorMessage)
                Exit Sub

            ElseIf Result.Text.Contains("missing=""") Then
                Fail(Msg("tag-fail", Page.Name), Msg("tag-deleted"))
                Exit Sub

            ElseIf AvoidText IsNot Nothing AndAlso Result.Text.Contains(AvoidText) Then
                Fail(Msg("tag-fail", Page.Name), Msg("tag-alreadytagged"))
                Exit Sub
            End If

            Dim Text As String = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>"))

            If ReplacePage Then
                Text = Tag
            ElseIf InsertAtEnd Then
                Text = Result.Text & LF & Tag
            Else
                Text = Tag & LF & Result.Text
            End If

            Result = PostEdit(Page, Text, Summary, Minor:=Config.MinorTags, Watch:=Config.WatchTags)

            If Result.Error Then
                Fail(Msg("tag-fail", Page.Name), Result.ErrorMessage)
                Exit Sub

            ElseIf State = States.Cancelled Then
                UndoEdit(Page)
                Exit Sub
            End If

            If Patrol Then
                Dim NewPatrolRequest As New PatrolRequest
                NewPatrolRequest.Page = Page
                NewPatrolRequest.Start()
            End If

            If NotifyRequest IsNot Nothing Then NotifyRequest.Start()

            Complete()
        End Sub

    End Class

    Class SpeedyRequest : Inherits Request

        'Tag a page for speedy deletion

        Public Page As Page, Criterion As SpeedyCriterion, Parameter As String
        Public AutoNotify, Notify As Boolean

        Protected Overrides Sub Process()
            LogProgress(Msg("speedy-progress", Page.Name))

            Dim Result As ApiResult = GetText(Page)

            If Result.Error Then
                Fail(, Result.ErrorMessage)
                Exit Sub

            ElseIf Result.Text.Contains("missing=""") Then
                Fail(Msg("speedy-fail", Page.Name), Msg("tag-deleted"))
                Exit Sub

            ElseIf Result.Text.Contains("{{db-") Then
                Fail(Msg("speedy-fail", Page.Name), Msg("tag-alreadytagged"))
                Exit Sub
            End If

            Dim Tag As String = "{{" & Criterion.Template
            If Parameter <> "" Then Tag &= "|" & Parameter
            Tag &= "}}"
            If Page.Space Is Space.Template Then Tag = "<noinclude>" & Tag & "</noinclude>"

            Dim Text As String = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>"))

            If Criterion.DisplayCode = "G10" Then Text = Tag Else Text = Tag & LF & Text

            Result = PostEdit(Page, Text, Config.SpeedySummary.Replace("$1", "[[WP:SD#" & Criterion.DisplayCode & _
                "|" & SpeedyCriteria(Criterion.DisplayCode).Description & "]]"), _
                Minor:=Config.MinorTags, Watch:=Config.WatchTags)

            If Result.Error Then Fail(, Result.ErrorMessage) Else Complete()
        End Sub

        Protected Overrides Sub Done()
            If Config.WatchTags Then
                If Not Watchlist.Contains(Page.SubjectPage) Then Watchlist.Add(Page.SubjectPage)
                MainForm.UpdateWatchButton()
            End If

            If State = States.Cancelled Then
                UndoEdit(Page)
            Else

                If Config.PatrolSpeedy Then
                    Dim NewPatrolRequest As New PatrolRequest
                    NewPatrolRequest.Page = Page
                    NewPatrolRequest.Start()
                End If

                If AutoNotify Then Notify = Criterion.Notify

                If Notify Then
                    If Page.Creator Is Nothing Then
                        'Get page history to find creator
                        Dim NewHistoryRequest As New HistoryRequest
                        NewHistoryRequest.Page = Page
                        NewHistoryRequest.Invoke()
                    End If

                    If Page.Creator Is Nothing Then
                        Log(Msg("notify-fail", Page.Name) & ": " & Msg("notify-unknowncreator"))

                    ElseIf Page.Creator IsNot User.Me Then
                        Dim NotifyRequest As New UserMessageRequest
                        NotifyRequest.Message = Criterion.Message.Replace("$1", Page.Name)
                        NotifyRequest.Title = Config.SpeedyMessageTitle.Replace("$1", Page.Name)
                        NotifyRequest.AvoidText = Page.Name
                        NotifyRequest.Summary = Config.SpeedyMessageSummary.Replace("$1", Page.Name)
                        NotifyRequest.User = Page.FirstEdit.User
                        NotifyRequest.Start()
                    End If
                End If
            End If
        End Sub

    End Class

    Class ReqProtectionRequest : Inherits Request

        'Request page protection

        Public Page As Page, Reason As String, Type As ProtectionType

        Protected Overrides Sub Process()
            LogProgress(Msg("reqprotection-progress", Page.Name))

            Dim Result As ApiResult = GetText(Config.ProtectionRequestPage, Section:="1")

            If Not Result.Text.Contains("{{" & Config.ProtectionRequestPage & "/PRheading}}") Then
                Fail(Msg("reqprotection-fail", Page.Name), Msg("reqprotection-badformat"))
                Exit Sub
            End If

            If Result.Text.Contains("|" & Page.Name & "}}") Then
                Fail(Msg("reqprotection-fail", Page.Name), Msg("reqprotection-alreadyrequested"))
                Exit Sub
            End If

            Dim Text As String = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>"))
            Dim Header As String

            If Page.IsArticleTalkPage Then
                Header = "===={{lat|" & Page.Name & "}}====" & LF
            ElseIf Page.IsTalkPage Then
                Header = "===={{lnt|" & Page.Space.Name & "|" & Page.Name & "}}====" & LF
            ElseIf Page.IsArticle Then
                Header = "===={{la|" & Page.Name & "}}====" & LF
            Else
                Header = "===={{ln|" & Page.Space.Name & "|" & Page.Name & "}}====" & LF
            End If

            Select Case Type
                Case ProtectionType.Full : Header &= "'''Full protection'''. "
                Case ProtectionType.Move : Header &= "'''Move protection'''. "
                Case ProtectionType.Semi : Header &= "'''Semi-protection'''. "
            End Select

            Text = Text.Substring(0, Text.IndexOf("====")) & Header & Reason & " ~~~~" _
                & LF & LF & Text.Substring(Text.IndexOf("===="))

            Dim ProtectionLevel As String = "protection"

            Select Case Type
                Case ProtectionType.Full : ProtectionLevel = "full protection"
                Case ProtectionType.Move : ProtectionLevel = "move protection"
                Case ProtectionType.Semi : ProtectionLevel = "semi-protection"
            End Select

            Result = PostEdit(Config.ProtectionRequestPage, Text, Config.ProtectionRequestSummary.Replace _
                ("$1", ProtectionLevel).Replace("$2", Page.Name), Section:="1", Minor:=Config.MinorReports, _
                Watch:=Config.WatchReports)

            If Result.Error _
                Then Fail(Msg("reqprotection-fail", Page.Text), Result.ErrorMessage) _
                Else Complete()

            If State = States.Cancelled Then UndoEdit(Config.ProtectionRequestPage)
        End Sub

    End Class

    Class UpdateWhitelistRequest : Inherits Request

        'Update the user whitelist

        Protected Overrides Sub Process()
            LogProgress("Updating whitelist...")

            Dim Result As ApiResult = GetText(Config.WhitelistLocation)

            If Result.Error Then
                Fail(, Result.ErrorMessage)
                Exit Sub
            End If

            Dim IgnoredUsernames As New List(Of String)

            For Each Item As String In Split(HtmlDecode(FindString(Result.Text, "<rev>", "</rev>")), LF)
                If Item.Length > 0 AndAlso Not (Item.Contains("{") OrElse Item.Contains("<")) _
                    AndAlso Not IgnoredUsernames.Contains(Item) Then IgnoredUsernames.Add(Item)
            Next Item

            For Each Item As String In WhitelistAutoChanges
                If Not IgnoredUsernames.Contains(Item) Then IgnoredUsernames.Add(Item)
            Next Item

            If Config.UpdateWhitelistManual Then
                For Each Item As String In WhitelistManualChanges
                    If Not IgnoredUsernames.Contains(Item) Then IgnoredUsernames.Add(Item)
                Next Item
            End If

            IgnoredUsernames.Sort(AddressOf CompareUsernames)

            Dim Text As String = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>"))
            Text = "{{/Header}}" & LF & "<pre>" & LF & String.Join(LF, IgnoredUsernames.ToArray) & LF & "</pre>"

            Result = PostEdit(Config.WhitelistLocation, Text, Config.WhitelistUpdateSummary, Minor:=True)

            If Result.Error Then Fail(, Result.ErrorMessage) Else Complete()
        End Sub

    End Class

End Namespace