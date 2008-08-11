Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class UserMessageRequest : Inherits Request

        Public User As User
        Public Summary, Message, Title, AvoidText As String
        Public Watch, Minor, AutoSign As Boolean

        Public Sub Start()
            LogProgress("Messaging '" & User.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData("User talk:" & User.Name)

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If AvoidText IsNot Nothing AndAlso Data.Text.ToLower.Contains(AvoidText.ToLower) Then
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

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If Config.WatchOther Then
                If Not Watchlist.Contains(GetPage("User:" & User.Name)) Then Watchlist.Add(GetPage("User:" & User.Name))
                MainForm.UpdateWatchButton()
            End If

            If State = States.Cancelled Then UndoEdit("User talk:" & User.Name) Else Complete()
        End Sub

        Private Sub ExistingMessage()
            Log("Did not post message '" & Title & "' for '" & User.Name & _
                "', as a message about the same thing was already present")
            Fail()
        End Sub

        Private Sub Failed()
            Log("Failed to post message '" & Title & "' for & '" & User.Name & "'")
            Fail()
        End Sub

    End Class

    Class WarningRequest : Inherits Request

        Public Edit As Edit, Type As String = "warning", Level As Integer

        Public Sub Start()
            If Edit IsNot Nothing AndAlso Edit.User.Level <> UserL.Ignore Then
                If Edit.User.Level = UserL.ReportedAIV Then
                    AlreadyReported()
                ElseIf Edit.User.Level = UserL.Blocked Then
                    AlreadyBlocked()
                Else
                    LogProgress("Warning '" & Edit.User.Name & "'...")
                    Dim RequestThread As New Thread(AddressOf Process)
                    RequestThread.IsBackground = True
                    RequestThread.Start()
                End If
            End If
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData(GetPage("User talk:" & Edit.User.Name))

            If Data.Error Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Data.Minor = Config.MinorWarnings
            Data.Watch = Config.WatchWarnings
            If Data.Text.Length > 1 Then Data.Text &= vbLf

            Dim ExistingWarnings As List(Of Warning) = ProcessUserTalk(Data.Text, Edit.User)
            Dim ExistingWarnLevel As UserL = UserL.None

            For Each Item As Warning In ExistingWarnings
                If Item.Time.AddHours(Config.WarningAge) > My.Computer.Clock.GmtTime _
                    AndAlso Item.Level > ExistingWarnLevel Then

                    ExistingWarnLevel = Item.Level
                    If Edit.User.WarnTime < Item.Time Then Edit.User.WarnTime = Item.Time
                End If
            Next Item

            If Edit.User.LastEdit IsNot Nothing AndAlso Edit.User.WarnTime > Edit.User.LastEdit.Time Then
                Callback(AddressOf OldEdit)
                Exit Sub
            End If

            If Edit.TypeToWarn IsNot Nothing Then Type = Edit.TypeToWarn

            If ExistingWarnings.Count = 1 AndAlso ExistingWarnLevel = UserL.WarnFinal _
                Then ExistingWarnLevel = UserL.Warn4im

            If Edit.User.Level < ExistingWarnLevel OrElse ExistingWarnLevel = UserL.Warn4im _
                Then Edit.User.Level = ExistingWarnLevel

            Dim LevelNeeded As UserL, WarningNeeded As String = ""

            If Level = 0 Then

                Dim FinalLevelReached As Boolean

                Select Case Edit.User.Level
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

            If Edit.LevelToWarn > LevelNeeded Then LevelNeeded = Edit.LevelToWarn
            If Edit.User.Level < LevelNeeded Then Edit.User.Level = LevelNeeded

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
            Data.Summary = WarnSummary.Replace("$1", Edit.Page.Name)

            If Config.MonthHeadings AndAlso Not (Data.Text.ToLower.Contains("== " & _
                GetMonthName(Date.UtcNow.Month).ToLower & " " & CStr(Date.UtcNow.Year) & " ==")) _
                AndAlso Not (Data.Text.ToLower.Contains("==" & GetMonthName(Date.UtcNow.Month).ToLower _
                & " " & CStr(Date.UtcNow.Year) & "==")) Then Data.Text &= "== " & _
                GetMonthName(Date.UtcNow.Month) & " " & CStr(Date.UtcNow.Year) & " ==" & vbCrLf & vbCrLf

            Data.Text &= _
                WarningNeeded.Replace("$1", Edit.Page.Name).Replace("$2", _
                SitePath & "wiki/" & Edit.Page.Name.Replace(" ", "_") & "?diff=" & Edit.Id)

            If WarningNeeded.Length > 0 Then Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub ReportNeeded()
            Log("Did not warn '" & Edit.User.Name & "' because they already have a final warning")

            If Administrator AndAlso Config.Block Then
                If Config.PromptForBlock Then MainForm.BlockUser(Edit.User)

            ElseIf Config.AIV AndAlso Config.AutoReport Then
                Dim NewReportRequest As New AIVReportRequest
                NewReportRequest.User = Edit.User
                NewReportRequest.Reason = Config.ReportReason
                NewReportRequest.Start()

            ElseIf Config.AIV AndAlso Config.PromptForReport Then
                MainForm.ReportUser(Edit.User, Edit)
            End If

            Complete()
        End Sub

        Private Sub Done()
            If Config.WatchWarnings Then
                If Not Watchlist.Contains(GetPage("User:" & Edit.User.Name)) _
                    Then Watchlist.Add(GetPage("User:" & Edit.User.Name))
                MainForm.UpdateWatchButton()
            End If

            If State = States.Cancelled Then UndoEdit("User talk:" & Edit.User.Name) Else Complete()
        End Sub

        Private Sub AlreadyReported()
            If Administrator AndAlso Config.Block Then
                If Config.PromptForBlock Then MainForm.BlockUser(Edit.User)
            Else
                'Already reported... but do we want it extended?
                If Config.AutoReport AndAlso Config.ReportLinkDiffs AndAlso Config.ExtendReports Then
                    Dim NewRequest As New AIVReportRequest
                    NewRequest.User = Edit.User
                    NewRequest.Edit = Edit
                    NewRequest.Reason = Config.ReportReason
                    NewRequest.Start()
                Else
                    Log("Did not warn '" & Edit.User.Name & "' because they have already been reported.")
                End If
            End If

            Fail()
        End Sub

        Private Sub AlreadyBlocked()
            Log("Did not warn '" & Edit.User.Name & "' because they have already been blocked.")
            Fail()
        End Sub

        Private Sub Failed()
            Log("Failed to warn '" & Edit.User.Name & "'.")
            Fail()
        End Sub

        Private Sub OldEdit()
            Log("Did not warn '" & Edit.User.Name & "', because they have not edited since their latest warning")
            Fail()
        End Sub

    End Class

    Class BlockNotificationRequest : Inherits Request

        Public User As User, Expiry, Reason, Template As String

        Public Sub Start()
            LogProgress("Notifying '" & User.Name & "' of block...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData("User talk:" & User.Name)

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

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf Failed) Else Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If State = States.Cancelled Then UndoEdit("User talk:" & User.Name) Else Complete()
        End Sub

        Private Sub Failed()
            Log("Failed to post block notification for '" & User.Name & "'")
            Fail()
        End Sub

    End Class

End Namespace
