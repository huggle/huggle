Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class UserMessageRequest : Inherits Request

        'Post a message on a user's discussion page

        Public User As User, Summary, Message, Title, AvoidText As String
        Public Watch As Boolean = Config.Watch("message"), Minor As Boolean = Config.Minor("message")
        Public AutoSign, SuppressAutoSummary As Boolean

        Protected Overrides Sub Process()
                LogProgress(Msg("usermessage-progress", User.Name))

                Dim Result As ApiResult = GetText(User.TalkPage)

                If Result.Error Then
                    Fail(Msg("usermessage-fail", User.Name), Result.ErrorMessage)
                    Exit Sub
                End If

                Dim Text As String = GetTextFromRev(Result.Text)
                If Text Is Nothing Then Text = ""

                If AvoidText IsNot Nothing AndAlso Text.ToLower.Contains(AvoidText.ToLower) Then
                    Fail(Msg("usermessage-fail", User.Name), Msg("usermessage-duplicate"))
                    Exit Sub
                End If

                Text &= LF & LF
                If Title <> "" Then Text &= "== " & Title & " ==" & LF & LF
                Text &= Message
                If AutoSign Then Text &= " ~~~~"

                Result = PostEdit(User.TalkPage, Text, Summary, Minor:=Minor, Watch:=Watch, _
                    SuppressAutoSummary:=SuppressAutoSummary)

                If Result.Error Then Fail(Msg("usermessage-fail", User.Name)) Else Complete()

                If State = States.Cancelled Then UndoEdit(User.TalkPage)
        End Sub

    End Class

    Class WarningRequest : Inherits Request

        Public Edit As Edit, Type As String = "warning", Level As Integer

        Protected Overrides Sub Process()

            If Edit.User.Ignored Then

            ElseIf Edit.User.Level = UserLevel.ReportedAIV Then
                AlreadyReported()

            ElseIf Edit.User.Level = UserLevel.Blocked Then
                Fail(Msg("warn-fail", Edit.User.Name), Msg("warn-alreadyblocked"))
                Exit Sub
            End If

            LogProgress(Msg("warn-progress", Edit.User.Name))

            Dim Result As ApiResult = GetText(Edit.User.TalkPage)

            If Result.Error Then
                Fail("warn-fail", Result.ErrorMessage)
                Exit Sub
            End If

            Dim Text As String = GetTextFromRev(Result.Text)

            If String.IsNullOrEmpty(Text) Then Text = "" Else Text &= LF

            Dim ExistingWarnings As List(Of Warning) = ProcessUserTalk(Text, Edit.User)
            Dim ExistingWarnLevel As UserLevel = UserLevel.None

            For Each Item As Warning In ExistingWarnings
                If Item.Time.AddHours(Config.WarningAge) > My.Computer.Clock.GmtTime _
                    AndAlso Item.Level > ExistingWarnLevel Then

                    ExistingWarnLevel = Item.Level
                    If Edit.User.WarnTime < Item.Time Then Edit.User.WarnTime = Item.Time
                End If
            Next Item

            If Edit.User.LastEdit IsNot Nothing AndAlso Edit.User.WarnTime > Edit.User.LastEdit.Time Then
                Fail(Msg("warn-fail", Edit.User.Name), Msg("warn-oldedit"))
                Exit Sub
            End If

            If Edit.TypeToWarn IsNot Nothing Then Type = Edit.TypeToWarn

            If ExistingWarnings.Count = 1 AndAlso ExistingWarnLevel = UserLevel.WarnFinal _
                Then ExistingWarnLevel = UserLevel.Warn4im

            If Edit.User.Level < ExistingWarnLevel OrElse ExistingWarnLevel = UserLevel.Warn4im _
                Then Edit.User.Level = ExistingWarnLevel

            Dim LevelNeeded As UserLevel, WarningNeeded As String = ""

            If Level = 0 Then

                Dim FinalLevelReached As Boolean

                Select Case Edit.User.Level
                    Case UserLevel.None, UserLevel.Message, UserLevel.Notification, UserLevel.Reverted, _
                        UserLevel.ReportedUAA

                        LevelNeeded = UserLevel.Warn1

                    Case UserLevel.Warning, UserLevel.Warn1
                        If Config.WarningMode <> "1" Then LevelNeeded = UserLevel.Warn2 Else FinalLevelReached = True

                    Case UserLevel.Warn2
                        If Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2" _
                            Then LevelNeeded = UserLevel.Warn3 Else FinalLevelReached = True

                    Case UserLevel.Warn3
                        If Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2" AndAlso Config.WarningMode <> "3" _
                            Then LevelNeeded = UserLevel.WarnFinal Else FinalLevelReached = True

                    Case UserLevel.Warn4im, UserLevel.WarnFinal
                        FinalLevelReached = True

                    Case UserLevel.ReportedAIV
                        Callback(AddressOf AlreadyReported)
                        Exit Sub

                    Case UserLevel.Blocked
                        Fail(Msg("warn-fail", Edit.User.Name), Msg("warn-alreadyblocked"))
                        Exit Sub
                End Select

                If FinalLevelReached Then
                    Callback(AddressOf ReportNeeded)
                    Exit Sub
                End If

            ElseIf Me.Level = 1 Then : LevelNeeded = UserLevel.Warn1
            ElseIf Me.Level = 2 Then : LevelNeeded = UserLevel.Warn2
            ElseIf Me.Level = 3 Then : LevelNeeded = UserLevel.Warn3
            ElseIf Me.Level = 4 Then : LevelNeeded = UserLevel.WarnFinal
            End If

            If Edit.LevelToWarn > LevelNeeded Then LevelNeeded = Edit.LevelToWarn
            If Edit.User.Level < LevelNeeded Then Edit.User.Level = LevelNeeded

            Dim WarnLevelName As String = "", WarnSummary As String = ""

            Select Case LevelNeeded
                Case UserLevel.Warn1 : WarnLevelName = "1" : WarnSummary = Config.WarnSummary
                Case UserLevel.Warn2 : WarnLevelName = "2" : WarnSummary = Config.WarnSummary2
                Case UserLevel.Warn3 : WarnLevelName = "3" : WarnSummary = Config.WarnSummary3
                Case UserLevel.WarnFinal
                    If ExistingWarnLevel < UserLevel.Warning Then WarnLevelName = "4im" Else WarnLevelName = "4"
                    WarnSummary = Config.WarnSummary4
            End Select

            WarningNeeded = Config.WarningMessages(Type & WarnLevelName)

            If Config.MonthHeadings AndAlso Not (Text.ToLower.Contains("== " & _
                GetMonthName(Date.UtcNow.Month).ToLower & " " & CStr(Date.UtcNow.Year) & " ==")) _
                AndAlso Not (Text.ToLower.Contains("==" & GetMonthName(Date.UtcNow.Month).ToLower _
                & " " & CStr(Date.UtcNow.Year) & "==")) _
                Then Text &= LF & "== " & GetMonthName(Date.UtcNow.Month) & " " & CStr(Date.UtcNow.Year) & " ==" & LF

            Text &= LF & WarningNeeded.Replace("$1", Edit.Page.Name).Replace("$2", _
                ShortSitePath() & UrlEncode(Edit.Page.Name.Replace(" ", "_")) & "?diff=" & Edit.Id)

            Result = PostEdit(Edit.User.TalkPage, Text, WarnSummary.Replace("$1", Edit.Page.Name), _
                Minor:=Config.Minor("warning"), Watch:=Config.Watch("warning"))

            If Result.Error Then Fail(Msg("warn-fail", Edit.User.Name), Result.ErrorMessage) Else Complete()

            If State = States.Cancelled Then UndoEdit(Edit.User.TalkPage)
        End Sub

        Private Sub ReportNeeded()
            If Config.Rights.Contains("block") AndAlso Config.Block Then
                If Config.PromptForBlock Then MainForm.BlockUser(Edit.User)

            ElseIf Config.AIV AndAlso Config.AutoReport Then
                Dim NewRequest As New VandalReportRequest
                NewRequest.User = Edit.User
                NewRequest.Reason = Config.VandalReportReason
                NewRequest.Start()

            ElseIf Config.AIV AndAlso Config.PromptForReport Then
                MainForm.ReportUser(Edit.User, Edit)
            End If

            Fail(Msg("warn-fail", Edit.User.Name), Msg("warn-alreadyfinal"))
        End Sub

        Private Sub AlreadyReported()
            If Config.Rights.Contains("block") AndAlso Config.Block Then
                If Config.PromptForBlock Then MainForm.BlockUser(Edit.User)
            Else
                'Already reported... but do we want it extended?
                If Config.AutoReport AndAlso Config.ReportLinkDiffs AndAlso Config.ExtendReports Then
                    Dim NewRequest As New VandalReportRequest
                    NewRequest.User = Edit.User
                    NewRequest.Edit = Edit
                    NewRequest.Reason = Config.VandalReportReason
                    NewRequest.Start()
                End If
            End If

            Fail(Msg("warn-fail", Edit.User.Name), Msg("warn-alreadyreported"))
        End Sub

    End Class

    Class BlockNotificationRequest : Inherits Request

        Public User As User, Expiry, Reason, Template As String

        Protected Overrides Sub Process()
            LogProgress(Msg("blocknotify-progress", User.Name))

            Dim Result As ApiResult = GetText(User.TalkPage)

            If Result.Error Then
                Fail(Msg("blocknotify-fail", User.Name))
                Exit Sub
            End If

            Dim Text As String = GetTextFromRev(Result.Text)

            If Template Is Nothing Then
                If (Expiry = "indefinite" OrElse Expiry = "infinite") _
                    Then Text &= LF & Config.BlockMessageIndef.Replace("$1", Reason) _
                    Else Text &= LF & Config.BlockMessage.Replace("$2", Reason).Replace("$1", Expiry)
            Else
                Text &= LF & Template & " ~~~~"
            End If

            Result = PostEdit(User.TalkPage, Text, Config.BlockSummary, _
                Minor:=Config.Minor("blocknote"), Watch:=Config.Watch("blocknote"))

            If Result.Error Then Fail(Msg("blocknotify-fail", User.Name), Result.ErrorMessage) Else Complete()

            If State = States.Cancelled Then UndoEdit(User.TalkPage)
        End Sub

    End Class

End Namespace
