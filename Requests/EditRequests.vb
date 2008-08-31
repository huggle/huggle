Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class EditRequest : Inherits Request

        'Make an arbitrary edit

        Public Page As Page, Text, Summary As String, Minor, Watch, NoAutoSummary As Boolean

        Public Sub Start(Optional ByVal Done As RequestCallback = Nothing)
            _Done = Done
            LogProgress("Editing '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData(Page)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Data.Minor = Minor
            Data.Watch = Watch
            Data.Text = Text
            Data.Summary = Summary
            Data.NoAutoSummary = NoAutoSummary

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If State = States.Cancelled Then UndoEdit(Page) Else Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class TagRequest : Inherits Request

        'Tag a page

        Public Page As Page, NotifyRequest As UserMessageRequest
        Public Tag, Summary, AvoidText As String
        Public ReplacePage, Patrol, InsertAtEnd As Boolean

        Public Sub Start()
            LogProgress("Tagging '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData(Page)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub

            ElseIf Data.Creating Then
                Callback(AddressOf PageDeleted)
                Exit Sub

            ElseIf AvoidText IsNot Nothing AndAlso Data.Text.Contains(AvoidText) Then
                Callback(AddressOf AlreadyTagged)
                Exit Sub
            End If

            Data.Watch = Config.WatchTags
            Data.Minor = Config.MinorTags
            Data.Summary = Summary

            If ReplacePage Then
                Data.Text = Tag
            ElseIf InsertAtEnd Then
                Data.Text &= LF & Tag
            Else
                Data.Text = Tag & LF & Data.Text
            End If

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If Config.WatchTags Then
                If Not Watchlist.Contains(Page.SubjectPage) Then Watchlist.Add(Page.SubjectPage)
                MainForm.UpdateWatchButton()
            End If

            If Patrol Then
                Dim NewPatrolRequest As New PatrolRequest
                NewPatrolRequest.Page = Page
                NewPatrolRequest.Start()
            End If

            If NotifyRequest IsNot Nothing Then NotifyRequest.Start()

            If State = States.Cancelled Then UndoEdit(Page) Else Complete()
        End Sub

        Private Sub AlreadyTagged()
            Log("Did not tag '" & Page.Name & "', as the page was already tagged")
            Fail()
        End Sub

        Private Sub PageDeleted()
            Log("Did not tag '" & Page.Name & "', as the page was deleted")
            Fail()
        End Sub

        Private Sub Failed()
            Log("Failed to tag '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

    Class SpeedyRequest : Inherits Request

        'Tag a page for speedy deletion

        Public Page As Page, Criterion As SpeedyCriterion, Parameter As String
        Public AutoNotify, Notify As Boolean

        Public Sub Start()
            LogProgress("Tagging '" & Page.Name & "' for speedy deletion...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData(Page)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub

            ElseIf Data.Creating Then
                Callback(AddressOf PageDeleted)
                Exit Sub

            ElseIf Data.Text.Contains("{{db-") Then
                Callback(AddressOf AlreadyTagged)
                Exit Sub
            End If

            Dim Tag As String = "{{" & Criterion.Template
            If Parameter <> "" Then Tag &= "|" & Parameter
            Tag &= "}}"

            Data.Watch = Config.WatchTags
            Data.Minor = Config.MinorTags
            Data.Summary = Config.SpeedySummary.Replace("$1", _
                "[[WP:SD#" & Criterion.DisplayCode & "|" & SpeedyCriteria(Criterion.DisplayCode).Description & "]]")

            If Criterion.DisplayCode = "G10" Then Data.Text = Tag Else Data.Text = Tag & LF & Data.Text

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If Config.WatchTags Then
                If Not Watchlist.Contains(Page.SubjectPage) Then Watchlist.Add(Page.SubjectPage)
                MainForm.UpdateWatchButton()
            End If

            If State = States.Cancelled Then
                UndoEdit(Page)
            Else
                Complete()

                If Config.PatrolSpeedy Then
                    Dim NewPatrolRequest As New PatrolRequest
                    NewPatrolRequest.Page = Page
                    NewPatrolRequest.Start()
                End If

                If AutoNotify Then Notify = Criterion.Notify

                If Notify Then
                    If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing Then
                        DoNotify()
                    Else
                        Dim NewHistoryRequest As New HistoryRequest
                        NewHistoryRequest.Page = Page
                        NewHistoryRequest.Start(AddressOf DoNotify)
                    End If
                End If
            End If
        End Sub

        Private Sub DoNotify(Optional ByVal Result As Request.Output = Nothing)
            If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing Then
                Dim NotifyRequest As New UserMessageRequest
                NotifyRequest.Message = Criterion.Message.Replace("$1", Page.Name)
                NotifyRequest.Title = Config.SpeedyMessageTitle.Replace("$1", Page.Name)
                NotifyRequest.AvoidText = Page.Name
                NotifyRequest.Summary = Config.SpeedyMessageSummary.Replace("$1", Page.Name)
                NotifyRequest.User = Page.FirstEdit.User
                NotifyRequest.Start()
            End If
        End Sub

        Private Sub PageDeleted()
            Log("Did not tag '" & Page.Name & "' for speedy deletion, as the page was deleted")
            Fail()
        End Sub

        Private Sub AlreadyTagged()
            Log("Did not tag '" & Page.Name & "' for speedy deletion, as the page was already tagged")
            Fail()
        End Sub

        Private Sub Failed()
            Log("Failed to tag '" & Page.Name & "' for speedy deletion")
            Fail()
        End Sub

    End Class

    Class ReqProtectionRequest : Inherits Request

        'Request page protection

        Public Page As Page, Reason As String, Type As ProtectionType

        Public Sub Start()
            LogProgress("Requesting protection of '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData(Config.ProtectionRequestPage, Section:=1)

            If Data.Error OrElse Not Data.Text.Contains("{{" & Config.ProtectionRequestPage & "/PRheading}}") Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If Data.Text.Contains("|" & Page.Name & "}}") Then
                Callback(AddressOf AlreadyRequested)
                Exit Sub
            End If

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

            Data.Text = Data.Text.Substring(0, Data.Text.IndexOf("====")) & Header & Reason & " ~~~~" _
                & LF & LF & Data.Text.Substring(Data.Text.IndexOf("===="))

            Select Case Type
                Case ProtectionType.Full : Data.Summary = "full protection"
                Case ProtectionType.Move : Data.Summary = "move protection"
                Case ProtectionType.Semi : Data.Summary = "semi-protection"
            End Select

            Data.Summary = Config.ProtectionRequestSummary.Replace("$1", Data.Summary).Replace("$2", Page.Name)

            Data.Minor = Config.MinorReports
            Data.Watch = Config.WatchReports

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If Config.WatchReports Then
                If Not Watchlist.Contains(GetPage(Config.ProtectionRequestPage)) _
                    Then Watchlist.Add(GetPage(Config.ProtectionRequestPage))
                MainForm.UpdateWatchButton()
            End If

            Complete()
            If State = States.Cancelled Then UndoEdit(Config.ProtectionRequestPage) Else Complete()
        End Sub

        Private Sub AlreadyRequested()
            Log("Did not request protection of '" & Page.Name & _
                "' because there is already a protection request for that page")
            Fail()
        End Sub

        Private Sub Failed()
            Log("Failed to request protection of '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

    Class AIVReportRequest : Inherits Request

        Public User As User, Edit As Edit, Reason As String

        Public Sub Start()
            If Config.AIVLocation IsNot Nothing AndAlso Config.AIVLocation.Length > 0 Then
                Dim GotContribs As Boolean

                If User.FirstEdit IsNot Nothing Then
                    GotContribs = True

                ElseIf User.LastEdit IsNot Nothing Then
                    Dim Contrib As Edit = User.LastEdit, i As Integer = 0

                    While Contrib.PrevByUser IsNot Nothing AndAlso Contrib.PrevByUser IsNot NullEdit
                        If Contrib.Time.AddHours(Config.WarningAge) < Date.UtcNow Then
                            GotContribs = True
                            Exit While
                        End If

                        i += 1
                        Contrib = Contrib.PrevByUser
                    End While

                    If i >= Config.ContribsBlockSize - 1 Then GotContribs = True
                End If

                If GotContribs Then
                    LogProgress("Reporting '" & User.Name & "'...")

                    Dim RequestThread As New Thread(AddressOf Process)
                    RequestThread.IsBackground = True
                    RequestThread.Start()
                Else
                    Dim NewRequest As New ContribsRequest
                    NewRequest.User = User
                    NewRequest.ReportWhenDone = Me
                    NewRequest.Start()
                End If
            End If
        End Sub

        Private Sub Process()
            'Check bot report subpage for report of user
            If Config.AIVBotLocation IsNot Nothing Then
                Result = GetPageText(Config.AIVBotLocation)
                Result = Result.ToLower.Replace("_", " ")

                If Result.ToLower.Contains("{{vandal|" & User.Name.ToLower & "}}") _
                    OrElse Result.ToLower.Contains("{{ipvandal|" & User.Name.ToLower & "}}") Then

                    If User.Level < UserLevel.ReportedAIV Then User.Level = UserLevel.ReportedAIV
                    Callback(AddressOf AlreadyReported)
                    Exit Sub
                End If
            End If

            Dim Data As EditData = GetEditData(GetPage(Config.AIVLocation))

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If Data.Text.ToLower.Contains("{{vandal|" & User.Name.ToLower & "}}") _
                OrElse Data.Text.ToLower.Contains("{{ipvandal|" & User.Name.ToLower & "}}") Then

                Data.Text = ExtendReport(Data.Text)
                Data.Summary = Config.ReportExtendSummary.Replace("$1", User.Name)
                Data.Minor = Config.MinorReports
                Data.Watch = Config.WatchReports

                If Data.Text Is Nothing Then
                    If User.Level < UserLevel.ReportedAIV Then User.Level = UserLevel.ReportedAIV
                    Callback(AddressOf AlreadyReported)
                    Exit Sub
                End If

                Data = PostEdit(Data)
                If Data.Error Then Callback(AddressOf AppendFailed) Else Callback(AddressOf Done)
                Exit Sub
            End If

            If User.Anonymous _
                Then Reason = "* {{IPvandal|" & User.Name & "}} – " & Reason _
                Else Reason = "* {{vandal|" & User.Name & "}} – " & Reason

            If Config.ReportLinkDiffs Then Reason &= LinkDiffs()
            If User.Level = UserLevel.Warn4im Then Reason &= " – " & Config.AivSingleNote

            Data.Text &= Reason & " – ~~~~"
            Data.Summary = Config.ReportSummary.Replace("$1", User.Name)
            Data.Minor = Config.MinorReports
            Data.Watch = Config.WatchReports

            Data = PostEdit(Data)
            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If Config.WatchReports Then
                If Not Watchlist.Contains(GetPage(Config.AIVLocation)) Then Watchlist.Add(GetPage(Config.AIVLocation))
                MainForm.UpdateWatchButton()
            End If

            If State = States.Cancelled Then UndoEdit(Config.AIVLocation) Else Complete()
        End Sub

        Private Sub AlreadyReported()
            Log("Did not report '" & User.Name & "' because they have already been reported")
            Fail()
        End Sub

        Private Sub Failed()
            Log("Failed to report '" & User.Name & "' to AIV")
            Fail()
        End Sub

        Private Sub AppendFailed()
            Fail()
        End Sub

        Private Function ExtendReport(ByVal Text As String) As String

            Dim ReportIndex As Integer, ReportLine As String = ""

            If Text.Contains("{{vandal|" & User.Name & "}}") _
                Then ReportIndex = Text.IndexOf("{{vandal|" & User.Name & "}}")
            If Text.Contains("{{IPvandal|" & User.Name & "}}") _
                Then ReportIndex = Text.IndexOf("{{IPvandal|" & User.Name & "}}")

            If ReportIndex > 0 Then ReportLine = Text.Substring(ReportIndex)
            If ReportLine.Contains(LF) Then ReportLine = ReportLine.Substring(0, ReportLine.IndexOf(LF))

            If Not (Config.ExtendReports AndAlso ReportLine.Contains("]</span>") AndAlso _
                ReportLine.Contains("vandalism, including <span class=""plainlinks"">") AndAlso _
                Edit IsNot Nothing AndAlso Not ReportLine.Contains("?diff=" & Edit.Id)) Then Return Nothing

            Dim DiffNumber As String = ReportLine.Substring(ReportLine.Substring(0, _
                ReportLine.IndexOf("]</span>")).LastIndexOf(" ") + 1)
            DiffNumber = DiffNumber.Substring(0, DiffNumber.IndexOf("]"))

            Dim Diffs As Integer
            If Not Integer.TryParse(DiffNumber, Diffs) Then Return Nothing
            If Diffs = 0 OrElse Diffs >= Config.MaxAIVDiffs Then Return Nothing

            ReportLine = ReportLine.Substring(0, ReportLine.IndexOf("]</span>") + 1) & ", [" _
                & Config.SitePath & "wiki/" & UrlEncode(Edit.Page.Name.Replace(" ", "_")) & "?diff=" _
                & Edit.Id & " " & CStr(Diffs) & "]" & _
                ReportLine.Substring(ReportLine.IndexOf("]</span>") + 1)

            Dim AfterReport As String = Text.Substring(ReportIndex)
            If AfterReport.Contains(LF) Then AfterReport = AfterReport.Substring(AfterReport.IndexOf(LF)) _
                Else AfterReport = ""

            Return Text.Substring(0, ReportIndex) & ReportLine & AfterReport
        End Function

        Private Function LinkDiffs() As String

            Dim RevertedEdits As New List(Of Edit), Edit As Edit = User.LastEdit

            While Edit IsNot Nothing AndAlso Edit IsNot NullEdit AndAlso Edit.Time.AddHours(3) > Date.UtcNow

                If Edit.Next IsNot Nothing AndAlso Edit.Next.Type = EditType.Revert Then RevertedEdits.Add(Edit)
                Edit = Edit.PrevByUser
            End While

            If RevertedEdits.Count = 0 Then Return Nothing

            Dim Links As String = ", including <span class=""plainlinks"">"

            For i As Integer = 0 To Math.Min(Config.MaxAIVDiffs - 1, RevertedEdits.Count - 1)

                Links &= "[" & Config.SitePath & "wiki/" & RevertedEdits(i).Page.Name.Replace(" ", "_") _
                    & "?diff=" & RevertedEdits(i).Id & " " & CStr(i + 1) & "]"
                If i < Math.Min(Config.MaxAIVDiffs - 1, RevertedEdits.Count - 1) Then Links &= ", "
            Next i

            Return Links & "</span>"
        End Function

    End Class

    Class UAAReportRequest : Inherits Request

        'Post a UAA report

        Public User As User, Reason As String

        Public Sub Start()
            If Config.UAALocation IsNot Nothing AndAlso Config.UAALocation.Length > 0 Then
                LogProgress("Reporting '" & User.Name & "'...")

                Dim RequestThread As New Thread(AddressOf Process)
                RequestThread.IsBackground = True
                RequestThread.Start()
            End If
        End Sub

        Private Sub Process()
            'Check bot report subpage for report of user
            If Config.UAABotLocation IsNot Nothing Then
                Result = GetPageText(Config.UAABotLocation)
                Result = Result.ToLower.Replace("_", " ")

                If Result.ToLower.Contains("{{userlinks|" & User.Name.ToLower & "}}") Then
                    If User.Level < UserLevel.ReportedUAA Then User.Level = UserLevel.ReportedUAA
                    Callback(AddressOf AlreadyReported)
                    Exit Sub
                End If
            End If

            Dim Data As EditData = GetEditData(GetPage(Config.UAALocation))

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If Data.Text.ToLower.Contains("{{userlinks|" & User.Name & "}}") Then
                If User.Level < UserLevel.ReportedUAA Then User.Level = UserLevel.ReportedUAA
                Callback(AddressOf AlreadyReported)
                Exit Sub
            End If

            Data.Text &= LF & "* {{userlinks|" & User.Name & "}} – " & Reason & " – ~~~~"
            Data.Summary = Config.ReportSummary.Replace("$1", User.Name)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If Config.WatchReports Then
                If Not Watchlist.Contains(GetPage(Config.UAALocation)) Then Watchlist.Add(GetPage(Config.UAALocation))
                MainForm.UpdateWatchButton()
            End If

            If State = States.Cancelled Then UndoEdit(Config.UAALocation) Else Complete()
        End Sub

        Private Sub AlreadyReported()
            Log("Did not post UAA report for '" & User.Name & "' because they have already been reported")
            Fail()
        End Sub

        Private Sub Failed()
            Log("Failed to report '" & User.Name & "' to UAA")
            Fail()
        End Sub

    End Class

    Class UpdateWhitelistRequest : Inherits Request

        'Update the user whitelist

        Public Sub Start()
            LogProgress("Updating whitelist...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData(Config.WhitelistLocation)

            If Data.Error Then
                Callback(AddressOf Done)
                Exit Sub
            End If

            Dim IgnoredUsernames As New List(Of String)

            Dim Items As String() = Data.Text.Split(LF)

            For Each Item As String In Items
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

            Data.Text = "{{/Header}}" & LF & "<pre>" & LF & _
                String.Join(LF, IgnoredUsernames.ToArray) & LF & "</pre>"
            Data.Summary = Config.WhitelistUpdateSummary
            Data.Minor = True

            Data = PostEdit(Data)
            'If the edit fails call fail, if it is done call done
            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If State = States.Cancelled Then UndoEdit(Config.WhitelistLocation) Else Complete()
            ClosingForm.WhitelistDone()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

    Class TrrReportRequest : Inherits Request

        'Report a three-revert rule violation

        Public User As User, Page As Page
        Public BaseEdit As Edit, Reverts As List(Of Edit)
        Public Message As String, Warn As Boolean

        Private Warning As Edit

        Public Sub Start()
            LogProgress("Reporting " & User.Name & "...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData(Config.TrrReportLocation, , 1)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Dim Report As String = ""

            Report = "== [[User:" & User.Name & "|" & User.Name & "]] reported by [[User:" & User.Me.Name & _
                "|" & User.Me.Name & "]] (Result: ) ==" & CRLF & CRLF

            Report &= "* Page: {{article|" & Page.Name & "}}." & CRLF
            Report &= "* User: {{userlinks|" & User.Name & "}}" & CRLF & CRLF
            
            Report &= "* Revision reverted to: <span class=""plainlinks"">[{{fullurl:" & Page.Name & "|oldid=" & _
                BaseEdit.Id & "}} " & WikiTimestamp(BaseEdit.Time) & "]</span>" & CRLF

            For i As Integer = 0 To Reverts.Count - 1
                Report &= "* " & Ordinal(i + 1) & " revert: <span class=""plainlinks"">[{{fullurl:" & Page.Name & _
                    "|diff=" & Reverts(i).Id & "&oldid=prev}} " & WikiTimestamp(Reverts(i).Time) & "]</span>" & CRLF
            Next i

            Report &= CRLF

            If Warning IsNot Nothing Then Report &= "* Warning: <span class=""plainlinks"">[{{fullurl:" & _
                Warning.Page.Name & "|diff=" & Warning.Id & "&oldid=prev}} " & WikiTimestamp(Warning.Time) & _
                "]</span>" & CRLF & CRLF

            If Message IsNot Nothing Then Report &= Message & CRLF & CRLF

            Report &= "~~~~"

            Data.Text &= CRLF & Report
            Data.Summary = Config.ReportSummary.Replace("$1", User.Name)
            Data.Minor = Config.MinorReports
            Data.Watch = Config.WatchReports OrElse Watchlist.Contains(GetPage(Config.TrrReportLocation))

            Data = PostEdit(Data)
            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

End Namespace