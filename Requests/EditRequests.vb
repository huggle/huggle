Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Module EditRequests

    Class EditRequest : Inherits Request

        'Make an arbitrary edit

        Public Page As Page, Text, Summary As String, Minor, Watch As Boolean
        Private _Done As CallbackDelegate

        Public Sub Start(Optional ByVal Done As CallbackDelegate = Nothing)
            _Done = Done

            Log("Editing '" & Page.Name & "'...", Page, True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEdit(Page)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Data.Minor = Minor
            Data.Watch = Watch
            Data.Text = Text
            Data.Summary = Summary

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Success)
        End Sub

        Private Sub Success(ByVal O As Object)
            Delog(Page)
            If _Done IsNot Nothing Then _Done(True)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Cancelled Then UndoEdit(Page)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Delog(Page)
            If _Done IsNot Nothing Then _Done(False)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

    End Class

    Class TagRequest : Inherits Request

        'Tag a page

        Public ThisPage As Page, NotifyRequest As UserMessageRequest
        Public Tag, Summary, Avoid As String, BlankPage, Patrol, InsertAtEnd As Boolean

        Public Sub Start()
            Log("Tagging '" & ThisPage.Name & "'...", ThisPage, True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEdit(ThisPage)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub

            ElseIf Data.Creating Then
                Callback(AddressOf PageDeleted)
                Exit Sub

            ElseIf Avoid IsNot Nothing AndAlso Data.Text.Contains(Avoid) Then
                Callback(AddressOf AlreadyTagged)
                Exit Sub
            End If

            Data.Watch = Config.WatchTags
            Data.Minor = Config.MinorTags
            Data.Summary = Summary

            If BlankPage Then
                Data.Text = Tag
            ElseIf InsertAtEnd Then
                Data.Text &= vbLf & Tag
            Else
                Data.Text = Tag & vbLf & Data.Text
            End If

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Config.WatchTags Then
                If Not Watchlist.Contains(SubjectPage(ThisPage)) Then Watchlist.Add(SubjectPage(ThisPage))
                Main.UpdateWatchButton()
            End If

            If Patrol Then
                Dim NewPatrolRequest As New PatrolRequest
                NewPatrolRequest.Page = ThisPage
                NewPatrolRequest.Start()
            End If

            If NotifyRequest IsNot Nothing Then NotifyRequest.Start()

            Delog(ThisPage)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Cancelled Then UndoEdit(ThisPage)
        End Sub

        Private Sub AlreadyTagged(ByVal O As Object)
            Log("Did not tag '" & ThisPage.Name & "', as the page was already tagged")
            Delog(ThisPage)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub PageDeleted(ByVal O As Object)
            Log("Did not tag '" & ThisPage.Name & "', as the page was deleted")
            Delog(ThisPage)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Log("Failed to tag '" & ThisPage.Name & "'")
            Delog(ThisPage)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

    End Class

    Class SpeedyRequest : Inherits Request

        'Tag a page for speedy deletion

        Public Page As Page, Criterion As SpeedyCriterion, AutoNotify, Notify As Boolean, Parameter As String

        Public Sub Start()
            Log("Tagging '" & Page.Name & "' for speedy deletion...", Page, True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEdit(Page)

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

            If Criterion.DisplayCode = "G10" Then Data.Text = Tag Else Data.Text = Tag & vbLf & Data.Text

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Config.WatchTags Then
                If Not Watchlist.Contains(SubjectPage(Page)) Then Watchlist.Add(SubjectPage(Page))
                Main.UpdateWatchButton()
            End If

            Delog(Page)
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)

            If Cancelled Then
                UndoEdit(Page)
                Exit Sub
            End If

            If Config.PatrolSpeedy Then
                Dim NewPatrolRequest As New PatrolRequest
                NewPatrolRequest.Page = Page
                NewPatrolRequest.Start()
            End If

            If AutoNotify Then Notify = Criterion.Notify

            If Notify Then
                If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing Then
                    DoNotify(True)
                Else
                    Dim NewHistoryRequest As New HistoryRequest
                    NewHistoryRequest.ThisPage = Page
                    NewHistoryRequest.Start(AddressOf DoNotify)
                End If
            End If
        End Sub

        Private Sub DoNotify(ByVal Success As Boolean)
            If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing Then
                Dim NotifyRequest As New UserMessageRequest
                NotifyRequest.Message = Criterion.Message.Replace("$1", Page.Name)
                NotifyRequest.Title = Config.SpeedyMessageTitle.Replace("$1", Page.Name)
                NotifyRequest.Avoid = Page.Name
                NotifyRequest.Summary = Config.SpeedyMessageSummary.Replace("$1", Page.Name)
                NotifyRequest.ThisUser = Page.FirstEdit.User
                NotifyRequest.Start()
            End If
        End Sub

        Private Sub PageDeleted(ByVal O As Object)
            Delog(Page)
            Log("Did not tag '" & Page.Name & "' for speedy deletion, as the page was deleted")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub AlreadyTagged(ByVal O As Object)
            Delog(Page)
            Log("Did not tag '" & Page.Name & "' for speedy deletion, as the page was already tagged")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Delog(Page)
            Log("Failed to tag '" & Page.Name & "' for speedy deletion")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

    End Class

    Class ReqProtectionRequest : Inherits Request

        'Request page protection

        Public ThisPage As Page
        Public Reason As String
        Public Type As ProtectionType

        Public Sub Start()
            Log("Requesting protection of '" & ThisPage.Name & "'...", GetPage(Config.ProtectionRequestPage), True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEdit(GetPage(Config.ProtectionRequestPage), , "1")

            If Data.Error OrElse Not Data.Text.Contains("{{" & Config.ProtectionRequestPage & "/PRheading}}") Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If Data.Text.Contains("|" & ThisPage.Name & "}}") Then
                Callback(AddressOf AlreadyRequested)
                Exit Sub
            End If

            Dim Header As String

            If ThisPage.Namespace.ToLower = "talk" Then
                Header = "===={{lat|" & ThisPage.Name & "}}====" & vbLf
            ElseIf ThisPage.Namespace.ToLower.EndsWith("talk") Then
                Header = "===={{lnt|" & ThisPage.Namespace & "|" & ThisPage.Name & "}}====" & vbLf
            ElseIf ThisPage.Namespace = "" Then
                Header = "===={{la|" & ThisPage.Name & "}}====" & vbLf
            Else
                Header = "===={{ln|" & ThisPage.Namespace & "|" & ThisPage.Name & "}}====" & vbLf
            End If

            Select Case Type
                Case ProtectionType.Full : Header &= "'''Full protection'''. "
                Case ProtectionType.Move : Header &= "'''Move protection'''. "
                Case ProtectionType.Semi : Header &= "'''Semi-protection'''. "
            End Select

            Data.Text = Data.Text.Substring(0, Data.Text.IndexOf("====")) & Header & Reason & " ~~~~" _
                & vbLf & vbLf & Data.Text.Substring(Data.Text.IndexOf("===="))

            Select Case Type
                Case ProtectionType.Full : Data.Summary = "full protection"
                Case ProtectionType.Move : Data.Summary = "move protection"
                Case ProtectionType.Semi : Data.Summary = "semi-protection"
            End Select

            Data.Summary = Config.ProtectionRequestSummary.Replace("$1", Data.Summary).Replace("$2", ThisPage.Name)

            Data.Minor = Config.MinorReports
            Data.Watch = Config.WatchReports

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Config.WatchReports Then
                If Not Watchlist.Contains(GetPage(Config.ProtectionRequestPage)) _
                    Then Watchlist.Add(GetPage(Config.ProtectionRequestPage))
                Main.UpdateWatchButton()
            End If

            Delog(GetPage(Config.ProtectionRequestPage))
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Cancelled Then UndoEdit(GetPage(Config.ProtectionRequestPage))
        End Sub

        Private Sub AlreadyRequested(ByVal O As Object)
            Delog(GetPage(Config.ProtectionRequestPage))
            'Protection was already requested
            Log("Did not request protection of '" & ThisPage.Name & _
                "' because there is already a protection request for that page")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Delog(GetPage(Config.ProtectionRequestPage))
            Log("Failed to request protection of '" & ThisPage.Name & "'")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

    End Class

    Class AIVReportRequest : Inherits Request

        Public ThisUser As User, ThisEdit As Edit, Reason As String

        Public Sub Start()
            If Config.AIVLocation IsNot Nothing AndAlso Config.AIVLocation.Length > 0 Then
                Dim GotContribs As Boolean

                If ThisUser.FirstEdit IsNot Nothing Then
                    GotContribs = True

                ElseIf ThisUser.LastEdit IsNot Nothing Then
                    Dim Contrib As Edit = ThisUser.LastEdit, i As Integer = 0

                    While Contrib.PrevByUser IsNot Nothing AndAlso Contrib.PrevByUser IsNot NullEdit
                        If Contrib.Time.AddHours(Config.WarningAge) < Date.UtcNow Then
                            GotContribs = True
                            Exit While
                        End If

                        i += 1
                        Contrib = Contrib.PrevByUser
                    End While

                    If i >= ContribsBlockSize - 1 Then GotContribs = True
                End If

                If GotContribs Then
                    Log("Reporting '" & ThisUser.Name & "'...", GetPage(Config.AIVLocation), True)
                    Dim RequestThread As New Thread(AddressOf Process)
                    RequestThread.IsBackground = True
                    RequestThread.Start()
                Else
                    Dim NewRequest As New ContribsRequest
                    NewRequest.ThisUser = ThisUser
                    NewRequest.ReportWhenDone = Me
                    NewRequest.Start()
                End If
            End If
        End Sub

        Private Sub Process()
            Dim BotPageText As String = GetText(GetPage(Config.AIVBotLocation))

            If BotPageText.ToLower.Contains("{{vandal|" & ThisUser.Name.ToLower & "}}") _
                OrElse BotPageText.ToLower.Contains("{{ipvandal|" & ThisUser.Name.ToLower & "}}") Then

                If ThisUser.Level < UserL.ReportedAIV Then ThisUser.Level = UserL.ReportedAIV
                Callback(AddressOf AlreadyReported)
                Exit Sub
            End If

            Dim Data As EditData = GetEdit(GetPage(Config.AIVLocation))

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If Data.Text.ToLower.Contains("{{vandal|" & ThisUser.Name.ToLower & "}}") _
                OrElse Data.Text.ToLower.Contains("{{ipvandal|" & ThisUser.Name.ToLower & "}}") Then

                Data.Text = ExtendReport(Data.Text)
                Data.Summary = Config.ReportExtendSummary.Replace("$1", ThisUser.Name)
                Data.Minor = Config.MinorReports
                Data.Watch = Config.WatchReports

                If Data.Text Is Nothing Then
                    If ThisUser.Level < UserL.ReportedAIV Then ThisUser.Level = UserL.ReportedAIV
                    Callback(AddressOf AlreadyReported)
                    Exit Sub
                End If

                If Cancelled Then Exit Sub

                Data = PostEdit(Data)
                If Data.Error Then Callback(AddressOf AppendFailed) Else Callback(AddressOf Done)
                Exit Sub
            End If

            If ThisUser.Anonymous _
                Then Reason = "* {{IPvandal|" & ThisUser.Name & "}} – " & Reason _
                Else Reason = "* {{Vandal|" & ThisUser.Name & "}} – " & Reason

            If Config.ReportLinkDiffs Then Reason &= LinkDiffs()
            If ThisUser.Level = UserL.Warn4im Then Reason &= " – " & Config.AivSingleNote

            Data.Text &= Reason & " – ~~~~"
            Data.Summary = Config.ReportSummary.Replace("$1", ThisUser.Name)
            Data.Minor = Config.MinorReports
            Data.Watch = Config.WatchReports

            If Cancelled Then Exit Sub

            Data = PostEdit(Data)
            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Config.WatchReports Then
                If Not Watchlist.Contains(GetPage(Config.AIVLocation)) Then Watchlist.Add(GetPage(Config.AIVLocation))
                Main.UpdateWatchButton()
            End If

            Delog(GetPage(Config.AIVLocation))
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Cancelled Then UndoEdit(GetPage(Config.AIVLocation))
        End Sub

        Private Sub AlreadyReported(ByVal O As Object)
            Delog(GetPage(Config.AIVLocation))
            Log("Did not report '" & ThisUser.Name & "' because they have already been reported")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Delog(GetPage(Config.AIVLocation))
            Log("Failed to report '" & ThisUser.Name & "' to AIV")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub AppendFailed(ByVal O As Object)
            Delog(GetPage(Config.AIVLocation))
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Function ExtendReport(ByVal Text As String) As String

            Dim ReportIndex As Integer, ReportLine As String = ""

            If Text.Contains("{{vandal|" & ThisUser.Name & "}}") _
                Then ReportIndex = Text.IndexOf("{{vandal|" & ThisUser.Name & "}}")
            If Text.Contains("{{IPvandal|" & ThisUser.Name & "}}") _
                Then ReportIndex = Text.IndexOf("{{IPvandal|" & ThisUser.Name & "}}")

            If ReportIndex > 0 Then ReportLine = Text.Substring(ReportIndex)
            If ReportLine.Contains(vbLf) Then ReportLine = ReportLine.Substring(0, ReportLine.IndexOf(vbLf))

            If Not (Config.ExtendReports AndAlso ReportLine.Contains("]</span>") AndAlso _
                ReportLine.Contains("vandalism, including <span class=""plainlinks"">") AndAlso _
                ThisEdit IsNot Nothing AndAlso Not ReportLine.Contains("?diff=" & ThisEdit.Id)) Then Return Nothing

            Dim DiffNumber As String = ReportLine.Substring(ReportLine.Substring(0, _
                ReportLine.IndexOf("]</span>")).LastIndexOf(" ") + 1)
            DiffNumber = DiffNumber.Substring(0, DiffNumber.IndexOf("]"))

            Dim diffnextbNumber As Integer = CInt(Val(DiffNumber)) + 1
            If Not (diffnextbNumber > 1 AndAlso diffnextbNumber <= Config.MaxAIVDiffs) Then Return Nothing

            ReportLine = ReportLine.Substring(0, ReportLine.IndexOf("]</span>") + 1) & ", [" _
                & SitePath & "wiki/" & UrlEncode(ThisEdit.Page.Name.Replace(" ", "_")) & "?diff=" _
                & ThisEdit.Id & " " & CStr(diffnextbNumber) & "]" & _
                ReportLine.Substring(ReportLine.IndexOf("]</span>") + 1)

            Dim AfterReport As String = Text.Substring(ReportIndex)
            If AfterReport.Contains(vbLf) Then AfterReport = AfterReport.Substring(AfterReport.IndexOf(vbLf)) _
                Else AfterReport = ""

            Return Text.Substring(0, ReportIndex) & ReportLine & AfterReport
        End Function

        Private Function LinkDiffs() As String

            Dim RevertedEdits As New List(Of Edit), Edit As Edit = ThisUser.LastEdit

            While Edit IsNot Nothing AndAlso Edit IsNot NullEdit AndAlso Edit.Time.AddHours(3) > Date.UtcNow

                If Edit.[Next] IsNot Nothing AndAlso Edit.[Next].Type = EditType.Revert _
                    Then RevertedEdits.Add(Edit)

                Edit = Edit.PrevByUser
            End While

            If RevertedEdits.Count = 0 Then Return Nothing

            Dim Links As String = ", including <span class=""plainlinks"">"

            For i As Integer = 0 To Math.Min(Config.MaxAIVDiffs - 1, RevertedEdits.Count - 1)

                Links &= "[" & SitePath & "wiki/" & RevertedEdits(i).Page.Name.Replace(" ", "_") _
                    & "?diff=" & RevertedEdits(i).Id & " " & CStr(i + 1) & "]"
                If i < Math.Min(Config.MaxAIVDiffs - 1, RevertedEdits.Count - 1) Then Links &= ", "
            Next i

            Return Links & "</span>"
        End Function

    End Class

    Class UAAReportRequest : Inherits Request

        'Post a UAA report

        Public ThisUser As User, Reason As String

        Public Sub Start()
            If Config.UAALocation IsNot Nothing AndAlso Config.UAALocation.Length > 0 Then
                Log("Reporting '" & ThisUser.Name & "'...", GetPage(Config.UAALocation), True)
                Dim RequestThread As New Thread(AddressOf Process)
                RequestThread.IsBackground = True
                RequestThread.Start()
            End If
        End Sub

        Private Sub Process()

            If Config.UAABotLocation IsNot Nothing Then
                Dim BotPageText As String = GetText(GetPage(Config.UAABotLocation))

                If BotPageText.ToLower.Contains("{{userlinks|" & ThisUser.Name.ToLower & "}}") Then
                    If ThisUser.Level < UserL.ReportedUAA Then ThisUser.Level = UserL.ReportedUAA
                    Callback(AddressOf AlreadyReported)
                    Exit Sub
                End If
            End If

            Dim Data As EditData = GetEdit(GetPage(Config.UAALocation))

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If Data.Text.ToLower.Contains("{{userlinks|" & ThisUser.Name & "}}") Then
                If ThisUser.Level < UserL.ReportedUAA Then ThisUser.Level = UserL.ReportedUAA
                Callback(AddressOf AlreadyReported)
                Exit Sub
            End If

            Data.Text &= vbLf & "* {{userlinks|" & ThisUser.Name & "}} – " & Reason & " – ~~~~"
            Data.Summary = Config.ReportSummary.Replace("$1", ThisUser.Name)

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Config.WatchReports Then
                If Not Watchlist.Contains(GetPage(Config.UAALocation)) Then Watchlist.Add(GetPage(Config.UAALocation))
                Main.UpdateWatchButton()
            End If

            Delog(GetPage(Config.UAALocation))
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Cancelled Then UndoEdit(GetPage(Config.UAALocation))
        End Sub

        Private Sub AlreadyReported(ByVal O As Object)
            Delog(GetPage(Config.UAALocation))
            Log("Did not post UAA report for '" & ThisUser.Name & "' because they have already been reported")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Delog(GetPage(Config.UAALocation))
            Log("Failed to report '" & ThisUser.Name & "' to UAA")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

    End Class

    Class UpdateWhitelistRequest : Inherits Request

        'Update the user whitelist

        Public Sub Start()
            Log("Updating whitelist...", GetPage(Config.WhitelistLocation), True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEdit(GetPage(Config.WhitelistLocation))

            If Data.Error Then
                Callback(AddressOf Done)
                Exit Sub
            End If

            Dim Items As String() = Data.Text.Split(CChar(vbLf))

            For Each Item As String In Items
                If Item.Length > 0 AndAlso Not (Item.Contains("{") OrElse Item.Contains("<")) _
                    AndAlso Not GetUser(Item).Level = UserL.Ignore Then GetUser(Item).Level = UserL.Ignore
            Next Item

            Dim IgnoredUserNames As New List(Of String)

            For Each Item As User In AllUsers.Values
                If Item.Level = UserL.Ignore AndAlso Not Item.Anonymous Then IgnoredUserNames.Add(Item.Name)
            Next Item

            IgnoredUserNames.Sort()

            Data.Text = "{{/Header}}" & vbLf & "<pre>" & vbLf & Join(IgnoredUserNames.ToArray, vbLf) & vbLf & "</pre>"
            Data.Summary = Config.WhitelistUpdateSummary
            Data.Minor = True

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            Delog(GetPage(Config.WhitelistLocation))
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Cancelled Then UndoEdit(GetPage(Config.WhitelistLocation))

            ClosingForm.WhitelistDone()
        End Sub

    End Class

End Module