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
            If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing _
                AndAlso Page.FirstEdit.User IsNot User.Me Then

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

End Namespace