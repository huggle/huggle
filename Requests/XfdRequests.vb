Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    MustInherit Class XfdRequest : Inherits Request

        'Represents a deletion discussion request

        Public Page As Page, Reason As String, Notify As Boolean
        Protected LogPath As String

        Protected ReadOnly Property LogDate() As String
            Get
                Return CStr(Date.UtcNow.Year) & " " & GetMonthName(Date.UtcNow.Month) & " " & CStr(Date.UtcNow.Day)
            End Get
        End Property

        Protected Function GetSubpageName(ByVal Name As String, ByVal Path As String) As RequestResult
            'Check for previous nominations of the same page
            Dim Subpage As String = Name
            Dim Result As ApiResult = GetApi("action=query&list=allpages&apnamespace=4&apprefix=" & _
                UrlEncode((Path.Substring(Path.IndexOf(":") + 1) & "/" & Name).Replace(" ", "_")))

            If Result.Error Then Return New RequestResult(, Result.ErrorMessage)

            If Result.Text.Contains("<allpages>") Then
                Dim ResultText As String = FindString(Result.Text, "<allpages>", "</allpages>")

                If ResultText.Contains(Path & "/" & Name & """") Then
                    Subpage = Name & " (2nd nomination)"

                    Dim i As Integer = 2

                    While ResultText.Contains(Path & "/" & Name & " (" & Ordinal(i) & " nomination)")
                        i += 1
                        Subpage = Name & " (" & Ordinal(i) & " nomination)"
                    End While
                End If
            End If

            Return New RequestResult(Subpage)
        End Function

        Protected Sub RfdNeeded()
            Dim NewRfdRequest As New RfdRequest
            NewRfdRequest.Page = Page
            NewRfdRequest.Reason = Reason
            NewRfdRequest.Notify = Notify
            NewRfdRequest.Invoke()

            Complete()
        End Sub

        Protected Sub DoNotify()
            If Page.Creator Is Nothing Then
                'Get page history to find creator
                Dim NewHistoryRequest As New HistoryRequest
                NewHistoryRequest.Page = Page
                NewHistoryRequest.Invoke()
            End If

            If Page.Creator Is Nothing Then
                Log(Msg("notify-fail", Page.Name) & ": " & Msg("notify-unknowncreator"))

            ElseIf Page.Creator IsNot User.Me Then
                'Notify page creator of deletion discussion
                Dim NewRequest As New UserMessageRequest
                NewRequest.AvoidText = Page.Name
                NewRequest.Minor = Config.MinorNotifications
                NewRequest.Watch = Config.WatchNotifications
                NewRequest.User = Page.FirstEdit.User
                NewRequest.Title = Config.XfdMessageTitle.Replace("$1", Page.Name)
                NewRequest.Summary = Config.XfdMessageSummary.Replace("$1", Page.Name)
                NewRequest.Message = Config.XfdMessage.Replace("$1", Page.Name).Replace("$2", LogPath)
                NewRequest.Start()
            End If
        End Sub

        Protected Function TagPage(ByVal Tag As String, ByVal Avoid As String, ByVal Redirect As Boolean) _
            As RequestResult

            LogProgress(Msg("reqdelete-tagprogress", Page.Name))

            Dim Result As ApiResult = GetText(Page)

            If Result.Error Then
                Return New RequestResult(, Result.ErrorMessage)

            ElseIf Result.Text.Contains("missing=""") Then
                Return New RequestResult(, Msg("error-pagemissing"))
            End If

            Dim Text As String = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>"))

            If Text.ToLower.StartsWith("#redirect [[") Then
                Return New RequestResult(, "rfdneeded")

            ElseIf Text.Contains(Avoid) Then
                Return New RequestResult(, Msg("reqdelete-duplicate"))
            End If

            Text = Tag & LF & Text

            Result = PostEdit(Page, Text, Config.XfdSummary, _
                Minor:=Config.MinorTags, Watch:=Config.WatchTags)

            Return New RequestResult(Result.Text, Result.ErrorMessage)
        End Function

        Protected Function CreateDiscussionSubpage(ByVal PageName As String, ByVal Text As String) As RequestResult
            LogProgress(Msg("reqdelete-subpageprogress", Page.Name))

            Dim Result As ApiResult = PostEdit(Config.AfdLocation & "/" & PageName, Text, _
                Config.XfdDiscussionSummary.Replace("$1", Page.Name), _
                Minor:=Config.MinorOther, Watch:=Config.WatchOther)

            Return New RequestResult(Result.Text, Result.ErrorMessage)
        End Function

        Protected Delegate Function UpdateLogDelegate(ByVal Text As String) As String

        Protected Function UpdateLog(ByVal Callback As UpdateLogDelegate, ByVal Summary As String, _
            Optional ByVal Section As String = Nothing) As RequestResult

            LogProgress(Msg("reqdelete-logprogress", Page.Name))

            Dim Result As ApiResult = GetText(GetPage(LogPath), Section:=Section)

            If Result.Error Then
                Return New RequestResult(, Result.ErrorMessage)
                Exit Function
            End If

            Dim Text As String = HtmlDecode(FindString(Result.Text, "<rev>", "</rev>"))

            Text = Callback(Text)

            Result = PostEdit(GetPage(LogPath), Text, Summary, Section:=Section, _
                Minor:=Config.MinorOther, Watch:=Config.WatchOther)

            Return New RequestResult(Result.Text, Result.ErrorMessage)
        End Function

    End Class

    Class AfdRequest : Inherits XfdRequest

        'Nominate an article for deletion

        Public Category As String
        Private Subpage As String

        Protected Overrides Sub Process()
            LogPath = Config.AfdLocation & "/Log/" & LogDate & "#" & Page.Name

            'Get title of discussion subpage
            Dim Result As RequestResult = GetSubpageName(Page.Name, Config.AfdLocation)

            If Result.Error Then
                Fail(Msg("reqdelete-tagfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Subpage = Result.Text

            'Tag page
            Result = TagPage("{{subst:afd|" & Subpage & "}}", "{{AfDM|", Redirect:=False)

            If Result.Error Then
                Fail(Msg("reqdelete-tagfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            'Create discussion subpage
            Result = CreateDiscussionSubpage(Subpage, _
                "{{subst:afd2|pg=" & Page.Name & "|cat=" & Category & "|text=" & Reason & "}} ~~~~")

            If Result.Error Then
                Fail(Msg("reqdelete-subpagefail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            'Update log page
            Result = UpdateLog(AddressOf UpdateLogCallback, Config.XfdLogSummary.Replace("$1", Subpage))

            If Result.Error Then
                Fail(Msg("reqdelete-logfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            If Notify Then DoNotify()
            Complete()
        End Sub

        Protected Function UpdateLogCallback(ByVal Text As String) As String
            If Text.Contains("{{" & Config.AfdLocation) Then
                Text = Text.Substring(0, Text.IndexOf("{{" & Config.AfdLocation)) & _
                    "{{" & Config.AfdLocation & "/" & Subpage & "}}" & LF & _
                    Text.Substring(Text.IndexOf("{{" & Config.AfdLocation))
            Else
                Text &= LF & "{{" & Config.AfdLocation & "/" & Subpage & "}}"
            End If

            Return Text
        End Function

    End Class

    Class CfdRequest : Inherits XfdRequest

        'Nominate a category for deletion

        Protected Overrides Sub Process()
            LogPath = Config.CfdLocation & "/Log/" & LogDate() & "#" & Page.Name

            'Tag page
            Dim Result As RequestResult = TagPage("{{subst:cfd}}", "<!--BEGIN CFD TEMPLATE-->", Redirect:=False)

            If Result.Error Then
                Fail(Msg("reqdelete-tagfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            'Add discussion to log page
            Result = UpdateLog(AddressOf UpdateLogCallback, Config.XfdSummary.Replace("$1", LogPath), Section:="2")

            If Result.Error Then
                Fail(Msg("reqdelete-logfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            If Notify Then DoNotify()
            Complete()
        End Sub

        Protected Function UpdateLogCallback(ByVal Text As String) As String
            Return Text & LF & "{{subst:cfd2|" & Page.Name.Substring(Page.Name.IndexOf(":") + 1) & _
                "|text=" & Reason & " ~~~~}}"
        End Function

    End Class

    Class MfdRequest : Inherits XfdRequest

        'Nominate a page for deletion

        Private Subpage As String

        Protected Overrides Sub Process()
            LogPath = Config.MfdLocation & "#" & Page.Name

            'Get title of discussion subpage
            Dim Result As RequestResult = GetSubpageName(Page.Name, Config.AfdLocation)

            If Result.Error Then
                Fail(Msg("reqdelete-tagfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Subpage = Result.Text

            'Tag page
            Result = TagPage("{{subst:mfd|" & Subpage & "}}", "{{mfdtag|", Redirect:=False)

            If Result.Error Then
                Fail(Msg("reqdelete-tagfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            'Create discussion subpage
            Result = CreateDiscussionSubpage(Subpage, _
                "{{subst:mfd2|pg=" & Page.Name & "|text=" & Reason & "}} ~~~~")

            If Result.Error Then
                Fail(Msg("reqdelete-subpagefail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            'Update log page
            Result = UpdateLog(AddressOf UpdateLogCallback, Config.XfdLogSummary.Replace("$1", Subpage))

            If Result.Error Then
                Fail(Msg("reqdelete-logfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            If Notify Then DoNotify()
            Complete()
        End Sub

        Protected Function UpdateLogCallback(ByVal Text As String) As String
            'Add day header if necessary
            Dim DayHeader As String = "===[[" & CStr(Date.UtcNow.Year) & "-" & _
                CStr(Date.UtcNow.Month).PadLeft(2, "0"c) & "-" & CStr(Date.UtcNow.Day).PadLeft(2, "0"c) & "]]==="

            Text = Text.Replace("<!-- " & DayHeader & " -->", LF & DayHeader)
            Text = Text.Replace("<!--" & DayHeader & "-->", LF & DayHeader)

            If Text.Contains(DayHeader) Then
                Text = Text.Substring(0, Text.IndexOf(DayHeader) + 20) & LF & "{{" & _
                    Config.MfdLocation & "/" & Subpage & "}}" & _
                    Text.Substring(Text.IndexOf(DayHeader) + 20)

            ElseIf Text.Contains("===") Then
                Text = Text.Substring(0, Text.IndexOf("===")) & DayHeader & LF & "{{" & _
                    Config.MfdLocation & "/" & Subpage & "}}" & LF & _
                    Text.Substring(Text.IndexOf("==="))

            Else
                Text &= LF & "{{" & Config.MfdLocation & "/" & Subpage & "}}"
            End If

            Return Text
        End Function

    End Class

    Class IfdRequest : Inherits XfdRequest

        'Nominate an image for deletion

        Protected Overrides Sub Process()
            LogPath = Config.IfdLocation & "/Log/" & LogDate() & "#" & Page.Name

            'Tag page
            Dim Result As RequestResult = TagPage("{{subst:ifd|log=" & LogDate & "}}", "{{IfD doc}}", Redirect:=False)

            If Result.Error Then
                Fail(Msg("reqdelete-tagfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            'Add discussion to log page
            Result = UpdateLog(AddressOf UpdateLogCallback, Config.XfdLogSummary.Replace("$1", Page.Name))

            If Result.Error Then
                Fail(Msg("reqdelete-logfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            If Notify Then DoNotify()
            Complete()
        End Sub

        Protected Function UpdateLogCallback(ByVal Text As String) As String
            Return Text & LF & "{{subst:ifd2|" & Page.Name.Substring(Page.Name.IndexOf(":") + 1) & _
                "|uploader=" & Page.FirstEdit.User.Name & "|reason=" & Reason & "}} ~~~~"
        End Function

    End Class

    Class TfdRequest : Inherits XfdRequest

        'Nominate a template for deletion

        Protected Overrides Sub Process()
            LogPath = Config.TfdLocation & "/Log/" & LogDate() & "#" & Page.Name

            'Tag page
            Dim Result As RequestResult = _
                TagPage("<noinclude>{{tfd|" & Page.Name & "}}</noinclude>", "{{tfd|", Redirect:=False)

            If Result.Error Then
                Fail(Msg("reqdelete-tagfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            'Add discussion to log page
            Result = UpdateLog(AddressOf UpdateLogCallback, Config.XfdLogSummary.Replace("$1", Page.Name), Section:="1")

            If Result.Error Then
                Fail(Msg("reqdelete-logfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            If Notify Then DoNotify()
            Complete()
        End Sub

        Protected Function UpdateLogCallback(ByVal Text As String) As String
            If Text.Contains("====") Then
                Text = Text.Substring(0, Text.IndexOf("====")) & "{{subst:tfd2|" & Page.Name & _
                    "|text=" & Reason & " ~~~~}}" & LF & LF & Text.Substring(Text.IndexOf("===="))
            Else
                Text &= LF & "{{subst:tfd2|" & Page.Name & "|text=" & Reason & " ~~~~}}"
            End If

            Return Text
        End Function

    End Class

    Class RfdRequest : Inherits XfdRequest

        'Nominate a redirect for deletion

        Private Target As String

        Protected Overrides Sub Process()
            LogPath = Config.RfdLocation & "/Log/" & LogDate() & "#" & Page.Name & " .E2.86.92 " & Target

            'Tag page
            Dim Result As RequestResult = TagPage("{{rfd}}", "{{rfd}}", Redirect:=True)

            If Result.Error Then
                Fail(Msg("reqdelete-tagfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            'Add discussion to log page
            Result = UpdateLog(AddressOf UpdateLogCallback, Config.XfdLogSummary.Replace("$1", Page.Name))

            If Result.Error Then
                Fail(Msg("reqdelete-logfail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            If Notify Then DoNotify()
            Complete()
        End Sub

        Protected Function UpdateLogCallback(ByVal Text As String) As String
            If Text.Contains("====") Then
                Text = Text.Substring(0, Text.IndexOf("====")) & "{{subst:rfd2|redirect=" & Page.Name & "|target=" & _
                    Target & "|text=" & Reason & "}} ~~~~" & LF & LF & Text.Substring(Text.IndexOf("===="))
            Else
                Text &= LF & "{{subst:rfd2|redirect=" & Page.Name & "|target=" & Target & "|text=" & Reason & "}} ~~~~"
            End If

            Return Text
        End Function

    End Class

End Namespace
