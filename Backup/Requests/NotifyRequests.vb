Imports System.Threading
Imports System.Web.HttpUtility

Module NotifyRequests

    Class UserMessageRequest : Inherits Request

        Public ThisUser As User
        Public Summary, Message, Title, Avoid As String
        Public Watch, Minor, AutoSign As Boolean

        Public Sub Start()
            Log("Messaging '" & ThisUser.Name & "'...", GetPage("User talk:" & ThisUser.Name), True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEdit(GetPage("User talk:" & ThisUser.Name))

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If Avoid IsNot Nothing AndAlso Data.Text.ToLower.Contains(Avoid.ToLower) Then
                Callback(AddressOf ExistingMessage)
                Exit Sub
            End If

            Data.Text &= vbLf
            If Title <> "" Then Data.Text &= "== " & Title & " ==" & vbLf & vbLf
            Data.Text &= Message
            If AutoSign Then Data.Text &= " ~~~~"
            Data.Summary = Summary
            Data.Minor = Minor
            Data.Watch = Watch

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Config.WatchOther Then
                If Not Watchlist.Contains(GetPage("User:" & ThisUser.Name)) _
                    Then Watchlist.Add(GetPage("User:" & ThisUser.Name))
                Main.UpdateWatchButton()
            End If

            Delog(GetPage("User talk:" & ThisUser.Name))
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Cancelled Then UndoEdit(GetPage("User talk:" & ThisUser.Name))
        End Sub

        Private Sub ExistingMessage(ByVal O As Object)
            Log("Did not post message for '" & ThisUser.Name & "', as a message about the same thing was already present")
            Delog(GetPage("User talk:" & ThisUser.Name))
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Log("Failed to post message '" & Title & "' for & '" & ThisUser.Name & "'")
            Delog(GetPage("User talk:" & ThisUser.Name))
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

    End Class

    Class WarningRequest : Inherits Request

        Public ThisEdit As Edit, Type As String = "warning", Level As Integer

        Public Sub Start()
            If ThisEdit IsNot Nothing AndAlso ThisEdit.User.Level <> UserL.Ignore Then
                If ThisEdit.User.Level = UserL.ReportedAIV Then
                    AlreadyReported(Nothing)
                ElseIf ThisEdit.User.Level = UserL.Blocked Then
                    AlreadyBlocked(Nothing)
                Else
                    Log("Warning '" & ThisEdit.User.Name & "'...", GetPage("User talk:" & ThisEdit.User.Name), True)
                    Dim RequestThread As New Thread(AddressOf Process)
                    RequestThread.IsBackground = True
                    RequestThread.Start()
                End If
            End If
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEdit(GetPage("User talk:" & ThisEdit.User.Name))

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Data.Minor = Config.MinorWarnings
            Data.Watch = Config.WatchWarnings
            If Data.Text.Length > 1 Then Data.Text &= vbLf

            Dim ExistingWarnings As List(Of Warning) = ProcessUserTalk(Data.Text, ThisEdit.User)
            Dim ExistingWarnLevel As UserL = UserL.None

            For Each Item As Warning In ExistingWarnings
                If Item.Time.AddHours(Config.WarningAge) > My.Computer.Clock.GmtTime AndAlso Item.Level > ExistingWarnLevel Then
                    ExistingWarnLevel = Item.Level
                    If ThisEdit.User.WarnTime < Item.Time Then ThisEdit.User.WarnTime = Item.Time
                End If
            Next Item

            If ThisEdit.User.LastEdit IsNot Nothing AndAlso ThisEdit.User.WarnTime > ThisEdit.User.LastEdit.Time Then
                Callback(AddressOf OldEdit)
                Exit Sub
            End If

            If ThisEdit.TypeToWarn IsNot Nothing Then Type = ThisEdit.TypeToWarn

            If ExistingWarnings.Count = 1 AndAlso ExistingWarnLevel = UserL.WarnFinal _
                Then ExistingWarnLevel = UserL.Warn4im

            If ThisEdit.User.Level < ExistingWarnLevel OrElse ExistingWarnLevel = UserL.Warn4im _
                Then ThisEdit.User.Level = ExistingWarnLevel

            Dim LevelNeeded As UserL, WarningNeeded As String = ""

            If Level = 0 Then

                Dim FinalLevelReached As Boolean

                Select Case ThisEdit.User.Level
                    Case UserL.None, UserL.Message, UserL.Notification, UserL.Reverted, UserL.ReportedUAA
                        LevelNeeded = UserL.Warn1

                    Case UserL.Warning, UserL.Warn1
                        If Config.WarningMode <> "1" Then LevelNeeded = UserL.Warn2 Else FinalLevelReached = True

                    Case UserL.Warn2
                        If Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2" _
                            Then LevelNeeded = UserL.Warn3 Else FinalLevelReached = True

                    Case UserL.Warn3
                        If Config.WarningMode <> "1" AndAlso Config.WarningMode <> "2" AndAlso Config.WarningMode <> "3" _
                            Then LevelNeeded = UserL.WarnFinal Else FinalLevelReached = True

                    Case UserL.Warn4im, UserL.WarnFinal
                        FinalLevelReached = True

                    Case UserL.ReportedAIV
                        Callback(AddressOf AlreadyReported)
                        Exit Sub

                    Case UserL.Blocked
                        Callback(AddressOf AlreadyBlocked)
                        Exit Sub
                End Select

                If FinalLevelReached Then
                    Callback(AddressOf ReportNeeded)
                    Exit Sub
                End If

            ElseIf Me.Level = 1 Then : LevelNeeded = UserL.Warn1
            ElseIf Me.Level = 2 Then : LevelNeeded = UserL.Warn2
            ElseIf Me.Level = 3 Then : LevelNeeded = UserL.Warn3
            ElseIf Me.Level = 4 Then : LevelNeeded = UserL.WarnFinal
            End If

            If ThisEdit.LevelToWarn > LevelNeeded Then LevelNeeded = ThisEdit.LevelToWarn
            If ThisEdit.User.Level < LevelNeeded Then ThisEdit.User.Level = LevelNeeded

            Dim WarnLevelName As String = "", WarnSummary As String = ""

            Select Case LevelNeeded
                Case UserL.Warn1 : WarnLevelName = "1" : WarnSummary = Config.WarnSummary
                Case UserL.Warn2 : WarnLevelName = "2" : WarnSummary = Config.WarnSummary2
                Case UserL.Warn3 : WarnLevelName = "3" : WarnSummary = Config.WarnSummary3
                Case UserL.WarnFinal
                    If ExistingWarnLevel < UserL.Warning Then WarnLevelName = "4im" Else WarnLevelName = "4"
                    WarnSummary = Config.WarnSummary4
            End Select

            WarningNeeded = WarningMessages(Type & WarnLevelName)
            Data.Summary = WarnSummary.Replace("$1", ThisEdit.Page.Name)

            If Config.MonthHeadings AndAlso Not (Data.Text.ToLower.Contains("== " & _
                GetMonthName(Date.UtcNow.Month).ToLower & " " & CStr(Date.UtcNow.Year) & " ==")) _
                AndAlso Not (Data.Text.ToLower.Contains("==" & GetMonthName(Date.UtcNow.Month).ToLower _
                & " " & CStr(Date.UtcNow.Year) & "==")) Then Data.Text &= "== " & _
                GetMonthName(Date.UtcNow.Month) & " " & CStr(Date.UtcNow.Year) & " ==" & vbCrLf & vbCrLf

            Data.Text &= _
                WarningNeeded.Replace("$1", ThisEdit.Page.Name).Replace("$2", _
                SitePath & "wiki/" & ThisEdit.Page.Name.Replace(" ", "_") & "?diff=" & ThisEdit.Id)

            If Cancelled Then Exit Sub
            If WarningNeeded.Length > 0 Then Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub ReportNeeded(ByVal O As Object)
            Delog(GetPage("User talk:" & ThisEdit.User.Name))
            Log("Did not warn '" & ThisEdit.User.Name & "' because they already have a final warning")

            If Administrator AndAlso Config.Block Then
                If Config.PromptForBlock Then Main.BlockUser(ThisEdit.User)

            ElseIf Config.AIV AndAlso Config.AutoReport Then
                Dim NewReportRequest As New AIVReportRequest
                NewReportRequest.ThisUser = ThisEdit.User
                NewReportRequest.Reason = Config.ReportReason
                NewReportRequest.Start()

            ElseIf Config.AIV AndAlso Config.PromptForReport Then
                Main.ReportUser(ThisEdit.User, ThisEdit)
            End If
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Done(ByVal ResultObject As Object)
            If Config.WatchWarnings Then
                If Not Watchlist.Contains(GetPage("User:" & ThisEdit.User.Name)) _
                    Then Watchlist.Add(GetPage("User:" & ThisEdit.User.Name))
                Main.UpdateWatchButton()
            End If

            Delog(GetPage("User talk:" & ThisEdit.User.Name))
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            If Cancelled Then UndoEdit(GetPage("User talk:" & ThisEdit.User.Name))
        End Sub

        Private Sub AlreadyReported(ByVal O As Object)
            Delog(GetPage("User talk:" & ThisEdit.User.Name))

            If Administrator Then
                Main.BlockUser(ThisEdit.User)
            Else
                'Already reported... but do we want it extended?
                If Config.AutoReport AndAlso Config.ReportLinkDiffs AndAlso Config.ExtendReports Then
                    Dim NewRequest As New AIVReportRequest
                    NewRequest.ThisUser = ThisEdit.User
                    NewRequest.ThisEdit = ThisEdit
                    NewRequest.Reason = Config.ReportReason
                    NewRequest.Start()
                End If
            End If
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub AlreadyBlocked(ByVal O As Object)
            Delog(GetPage("User talk:" & ThisEdit.User.Name))
            Log("Did not warn '" & ThisEdit.User.Name & "' because they have already been blocked.")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Delog(GetPage("User talk:" & ThisEdit.User.Name))
            Log("Failed to warn '" & ThisEdit.User.Name & "'.")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub OldEdit(ByVal O As Object)
            Delog(GetPage("User talk:" & ThisEdit.User.Name))
            Log("Did not warn '" & ThisEdit.User.Name & "', because they have not edited since their latest warning")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub
    End Class

    Class BlockNotificationRequest : Inherits Request

        Public ThisUser As User, Expiry, Reason, Template As String

        Public Sub Start()
            Log("Notifying '" & ThisUser.Name & "' of block...", GetPage("User talk:" & ThisUser.Name), True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEdit(GetPage("User talk:" & ThisUser.Name))

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If Template Is Nothing Then
                If (Expiry = "indefinite" OrElse Expiry = "infinite") _
                    Then Data.Text &= vbLf & Config.BlockMessageIndef.Replace("$1", Reason) _
                    Else Data.Text &= vbLf & Config.BlockMessage.Replace("$2", Reason).Replace("$1", Expiry)
            Else

                Data.Text &= vbLf & Template & " ~~~~"
            End If

            Data.Minor = Config.MinorReports
            Data.Watch = Config.WatchReports
            Data.Summary = Config.BlockSummary

            If Cancelled Then Exit Sub
            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            Delog(GetPage("User talk:" & ThisUser.Name))
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Delog(GetPage("User talk:" & ThisUser.Name))
            Log("Failed to post block notification for '" & ThisUser.Name & "'")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

    End Class

End Module
