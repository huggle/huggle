Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class TrrReportRequest : Inherits Request

        'Report a three-revert rule violation

        Public User As User, Page As Page
        Public BaseEdit As Edit, Reverts As List(Of Edit)
        Public Message As String, Warn As Boolean

        Private Warning As Edit

        Protected Overrides Sub Process()
            LogProgress(Msg("report-progress", User.Name))

            Dim Result As ApiResult = GetText(Config.TRRLocation, Section:="1")

            If Result.Error Then
                Fail(Msg("report-fail", User.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Dim Report As String = CRLF

            Report &= "== [[User:" & User.Name & "|" & User.Name & "]] reported by [[User:" & User.Me.Name & _
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

            Result = PostEdit(Config.TRRLocation, Result.Text & Report, Config.ReportSummary.Replace("$1", User.Name), _
                Section:="1", Minor:=Config.Minor("report"), Watch:=Config.Watch("report"))

            If Result.Error _
                Then Fail(Msg("report-fail", User.Name), Result.ErrorMessage) _
                Else Complete()
        End Sub

    End Class

    Class VandalReportRequest : Inherits Request

        'Report vandalism

        Public User As User, Edit As Edit, Reason As String

        Protected Overrides Sub Process()
            LogProgress(Msg("report-progress", User.Name))

            'Load all user's contributions if necessary,
            'to ensure that any other edits from the user are checked

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

            If Not GotContribs Then
                Dim NewRequest As New ContribsRequest
                NewRequest.User = User

                Dim ContribsResult As RequestResult = NewRequest.Invoke

                If ContribsResult.Error Then
                    Fail(Msg("report-fail", User.Name), ContribsResult.ErrorMessage)
                    Exit Sub
                End If
            End If

            Dim Result As ApiResult, Text As String

            'Check bot report subpage for report of user
            If Config.AIVBotLocation IsNot Nothing Then
                Result = GetText(Config.AIVBotLocation)

                If Result.Error Then
                    Fail(Msg("report-fail", User.Name), Result.ErrorMessage)
                    Exit Sub
                End If

                Text = Result.Text.ToLower.Replace("_", " ")

                If Text.Contains("{{vandal|" & User.Name.ToLower & "}}") _
                    OrElse Text.Contains("{{vandal|1=" & User.Name.ToLower & "}}") _
                    OrElse Text.Contains("{{ipvandal|" & User.Name.ToLower & "}}") Then

                    If User.Level < UserLevel.ReportedAIV Then User.Level = UserLevel.ReportedAIV

                    If Result.Error _
                        Then Fail(Msg("report-fail", User.Name), Result.ErrorMessage) _
                        Else Fail(Msg("report-fail", User.Name), Msg("report-alreadyreported"))

                    Exit Sub
                End If
            End If

            Result = GetText(Config.AIVLocation)

            If Result.Error Then
                Fail(Msg("report-fail", User.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Text = HtmlDecode(FindString(Result.Text, "<rev", "</rev"))

            'Check for existing report
            If Text.ToLower.Contains("{{vandal|" & User.Name.ToLower & "}}") _
                OrElse Text.ToLower.Contains("{{vandal|1=" & User.Name.ToLower & "}}") _
                OrElse Text.ToLower.Contains("{{ipvandal|" & User.Name.ToLower & "}}") Then

                If User.Level < UserLevel.ReportedAIV Then User.Level = UserLevel.ReportedAIV

                If Config.ExtendReports Then
                    'Extend report
                    Dim ExtendedText As String = ExtendReport(Text)

                    If ExtendedText IsNot Nothing Then
                        Result = PostEdit(Config.AIVLocation, ExtendedText, _
                            Config.ReportExtendSummary.Replace("$1", User.Name), _
                            Minor:=Config.Minor("report"), Watch:=Config.Watch("report"))
                        If Result.Error Then Fail(Msg("report-fail", User.Name), Result.ErrorMessage) Else Complete()
                    Else
                        Fail(Msg("report-fail", User.Name), Msg("report-alreadyreported"))
                    End If
                Else
                    Fail(Msg("report-fail", User.Name), Msg("report-alreadyreported"))
                End If

                Exit Sub
            End If

            'Report user
            Dim Report As String = CRLF

            If User.Anonymous Then Report &= "* {{IPvandal|" _
                Else If User.Name.Contains("=") Then Report &= "* {{vandal|1=" _
                Else Report &= "* {{vandal|"

            Report &= User.Name & "}} – " & Reason
            If Config.ReportLinkDiffs Then Report &= LinkDiffs()
            If User.Level = UserLevel.Warn4im AndAlso Config.AivSingleNote IsNot Nothing _
                Then Report &= " – " & Config.AivSingleNote
            Report &= " – ~~~~"

            Result = PostEdit(Config.AIVLocation, Text & Report, Config.ReportSummary.Replace("$1", User.Name), _
                Minor:=Config.Minor("report"), Watch:=Config.Watch("report"))

            If Result.Error Then Fail(Msg("report-fail", User.Name), Result.ErrorMessage) Else Complete()

            If State = States.Cancelled Then UndoEdit(Config.AIVLocation)
        End Sub

        'Extend an existing report in the same format
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
            Diffs += 1

            ReportLine = ReportLine.Substring(0, ReportLine.IndexOf("]</span>") + 1) & ", [" _
                & ShortSitePath() & UrlEncode(Edit.Page.Name.Replace(" ", "_")) & "?diff=" _
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

                Links &= "[" & ShortSitePath() & RevertedEdits(i).Page.Name.Replace(" ", "_") _
                    & "?diff=" & RevertedEdits(i).Id & " " & CStr(i + 1) & "]"
                If i < Math.Min(Config.MaxAIVDiffs - 1, RevertedEdits.Count - 1) Then Links &= ", "
            Next i

            Return Links & "</span>"
        End Function

    End Class

    Class VandalReportEsRequest : Inherits Request

        'Report vandalism on es.wikipedia

        Public User As User, Edit As Edit, Reason As String

        Protected Overrides Sub Process()
            LogProgress(Msg("report-progress", User.Name))

            'Load all user's contributions if necessary,
            'to ensure that any other edits from the user are checked

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

            If Not GotContribs Then
                Dim NewRequest As New ContribsRequest
                NewRequest.User = User

                Dim ContribsResult As RequestResult = NewRequest.Invoke

                If ContribsResult.Error Then
                    Fail(Msg("report-fail", User.Name), ContribsResult.ErrorMessage)
                    Exit Sub
                End If
            End If

            Dim Result As ApiResult = GetText(Config.AIVLocation)

            If Result.Error Then
                Fail(Msg("report-fail", User.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Dim Text As String = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>"))

            If Text Is Nothing Then
                Fail(Msg("report-fail", User.Name))
                Exit Sub
            End If

            'Check for existing report
            If Text.ToLower.Contains("|" & User.Name.ToLower & "}}") Then
                If User.Level < UserLevel.ReportedAIV Then User.Level = UserLevel.ReportedAIV
                Fail(Msg("report-fail", User.Name), Msg("report-alreadyreported"))
                Exit Sub
            End If

            'Report user
            Dim Report As String = CRLF & CRLF & "=== " & User.Name & " ===" & CRLF & "* Posible vándalo: "

            If User.Anonymous Then Report &= "{{VándaloIP|" _
                Else If User.Name.Contains("=") Then Report &= "{{Vándalo|1=" _
                Else Report &= "{{Vándalo|"

            Report &= User.Name & "}}" & CRLF & "* Motivo de reporte: " & Reason

            If Config.ReportLinkDiffs Then Report &= LinkDiffs()

            Report &= CRLF & "* Usuario que reporta: ~~~~" & CRLF & "* Acción administrativa:" & CRLF

            Result = PostEdit(Config.AIVLocation, Text & Report, Config.ReportSummary.Replace("$1", User.Name), _
                Minor:=Config.Minor("report"), Watch:=Config.Watch("report"))

            If Result.Error Then Fail(Msg("report-fail", User.Name), Result.ErrorMessage) Else Complete()

            If State = States.Cancelled Then UndoEdit(Config.AIVLocation)
        End Sub

        Private Function LinkDiffs() As String

            Dim RevertedEdits As New List(Of Edit), Edit As Edit = User.LastEdit

            While Edit IsNot Nothing AndAlso Edit IsNot NullEdit AndAlso Edit.Time.AddHours(3) > Date.UtcNow

                If Edit.Next IsNot Nothing AndAlso Edit.Next.Type = EditType.Revert Then RevertedEdits.Add(Edit)
                Edit = Edit.PrevByUser
            End While

            If RevertedEdits.Count = 0 Then Return Nothing

            Dim Links As String = " - <span class=""plainlinks"">"

            For i As Integer = 0 To Math.Min(Config.MaxAIVDiffs - 1, RevertedEdits.Count - 1)

                Links &= "[" & ShortSitePath() & RevertedEdits(i).Page.Name.Replace(" ", "_") _
                    & "?diff=" & RevertedEdits(i).Id & " " & CStr(i + 1) & "]"
                If i < Math.Min(Config.MaxAIVDiffs - 1, RevertedEdits.Count - 1) Then Links &= ", "
            Next i

            Return Links & "</span>"
        End Function

    End Class

    Class UsernameReportRequest : Inherits Request

        'Report username

        Public User As User, Reason As String

        Protected Overrides Sub Process()
            LogProgress(Msg("report-progress", User.Name))

            Dim Result As ApiResult, Text As String

            'Check bot report subpage for report of user
            If Config.UAABotLocation IsNot Nothing Then
                Result = GetText(Config.UAABotLocation)

                Text = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>")).ToLower.Replace("_", " ")

                If Text.Contains("{{userlinks|" & User.Name.ToLower & "}}") _
                    OrElse Text.Contains("{{userlinks|1=" & User.Name.ToLower & "}}") Then

                    If User.Level < UserLevel.ReportedUAA Then User.Level = UserLevel.ReportedUAA

                    If Result.Error _
                        Then Fail(Msg("report-fail", User.Name), Result.ErrorMessage) _
                        Else Fail(Msg("report-fail", User.Name), Msg("report-alreadyreported"))

                    Exit Sub
                End If
            End If

            Result = GetText(Config.UAALocation)

            If Result.Error Then
                Fail(Msg("report-fail", User.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Text = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>")).ToLower.Replace("_", " ")

            'Check for existing report
            If Text.Contains("{{userlinks|" & User.Name & "}}") _
                OrElse Text.Contains("{{userlinks|1=" & User.Name & "}}") Then

                If User.Level < UserLevel.ReportedUAA Then User.Level = UserLevel.ReportedUAA
                Fail(Msg("report-fail", User.Name), Msg("report-alreadyreported"))
                Exit Sub
            End If

            'Report user
            Text &= LF & "* {{userlinks|"
            If User.Name.Contains("=") Then Text &= "1="
            Text &= User.Name & "}} – " & Reason & " – ~~~~"

            Result = PostEdit(Config.UAALocation, Text, Config.ReportSummary.Replace("$1", User.Name), _
                Minor:=Config.Minor("report"), Watch:=Config.Watch("report"))

            If Result.Error Then Fail(Msg("report-fail", User.Name), Result.ErrorMessage) Else Complete()

            If State = States.Cancelled Then UndoEdit(Config.UAALocation)
        End Sub

    End Class

End Namespace
