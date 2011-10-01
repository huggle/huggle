Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class TrrReportRequest : Inherits Request

        'Report a three-revert rule violation

        Public User As User, Page As Page
        Public BaseEdit As Edit, Reverts As List(Of Edit)
        Public Message As String, Warn As Boolean

        Private Warning As Edit

        Public Sub Start()
            If Config.TRR Then
                LogProgress("Reporting " & User.Name & "...")

                Dim RequestThread As New Thread(AddressOf Process)
                RequestThread.IsBackground = True
                RequestThread.Start()
            End If
        End Sub

        Private Sub Process()
            Dim Data As EditData = GetEditData(Config.TRRLocation, , 1)

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
            Data.Watch = Config.WatchReports OrElse Watchlist.Contains(GetPage(Config.TRRLocation))

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

    Class AIVReportRequest : Inherits Request

        Public User As User, Edit As Edit, Reason As String

        Public Sub Start()
            If Config.AIV Then
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
                    OrElse Result.ToLower.Contains("{{vandal|1=" & User.Name.ToLower & "}}") _
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
                OrElse Data.Text.ToLower.Contains("{{vandal|1=" & User.Name.ToLower & "}}") _
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

            If User.Anonymous Then Data.Text &= "* {{IPvandal|" _
                Else If User.Name.Contains("=") Then Data.Text &= "* {{vandal|1=" _
                Else Data.Text &= "* {{vandal|"

            Data.Text &= User.Name & "}} – " & Reason
            If Config.ReportLinkDiffs Then Data.Text &= LinkDiffs()
            If User.Level = UserLevel.Warn4im Then Data.Text &= " – " & Config.AivSingleNote

            Data.Text &= " – ~~~~"
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
            If Text.Contains("{{vandal|1=" & User.Name & "}}") _
                Then ReportIndex = Text.IndexOf("{{vandal|1=" & User.Name & "}}")
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
            If Config.UAA Then
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

                If Result.ToLower.Contains("{{userlinks|" & User.Name.ToLower & "}}") _
                    OrElse Result.ToLower.Contains("{{userlinks|1=" & User.Name.ToLower & "}}") Then

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

            If Data.Text.ToLower.Contains("{{userlinks|" & User.Name & "}}") _
                OrElse Data.Text.ToLower.Contains("{{userlinks|1=" & User.Name & "}}") Then

                If User.Level < UserLevel.ReportedUAA Then User.Level = UserLevel.ReportedUAA
                Callback(AddressOf AlreadyReported)
                Exit Sub
            End If

            Data.Text &= LF & "* {{userlinks|"
            If User.Name.Contains("=") Then Data.Text &= "1="
            Data.Text &= User.Name & "}} – " & Reason & " – ~~~~"
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

End Namespace
