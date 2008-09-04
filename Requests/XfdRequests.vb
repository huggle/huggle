Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class XfdRequest : Inherits Request

        Public Page As Page, Reason As String, Notify As Boolean

        Protected LogPath As String

        Protected Function LogDate() As String
            Return CStr(Date.UtcNow.Year) & " " & GetMonthName(Date.UtcNow.Month) & " " & CStr(Date.UtcNow.Day)
        End Function

        Protected Function GetNominationSubpage(ByVal Name As String, ByVal Path As String) As String
            'Check for previous nominations of the same page
            Dim Subpage As String = Name
            Dim Result As String = GetApi("action=query&format=xml&list=allpages&apnamespace=4&apprefix=" & _
                UrlEncode((Path.Substring(Path.IndexOf(":") + 1) & "/" & Name).Replace(" ", "_")))

            If Result IsNot Nothing AndAlso Result.Contains("<allpages>") Then
                Result = FindString(Result, "<allpages>", "</allpages>")

                If Result.Contains(Path & "/" & Name & """") Then
                    Subpage = Name & " (2nd nomination)"

                    Dim i As Integer = 2

                    While Result.Contains(Path & "/" & Name & " (" & Ordinal(i) & " nomination)")
                        i += 1
                        Subpage = Name & " (" & Ordinal(i) & " nomination)"
                    End While
                End If
            End If

            Return Subpage
        End Function

        Protected Sub RfdNeeded(ByVal DataObject As Object)
            Dim NewRfdRequest As New RfdRequest
            NewRfdRequest.Page = Page
            NewRfdRequest.Data = CType(DataObject, EditData)
            NewRfdRequest.Reason = Reason
            NewRfdRequest.Notify = Notify
            NewRfdRequest.Start()

            Complete()
        End Sub

        Protected Sub PageDeleted()
            Log("Did not tag '" & Page.Name & "' for deletion discussion, because the page does not exist")
            Fail()
        End Sub

        Protected Sub AlreadyTagged()
            Log("Did not tag '" & Page.Name & "' for deletion discussion, because it has already been tagged.")
            Fail()
        End Sub

        Protected Sub DoNotify(Optional ByVal Result As Request.Output = Nothing)
            If (Result Is Nothing OrElse Result.Success) AndAlso Page.FirstEdit IsNot Nothing _
                AndAlso Page.FirstEdit.User IsNot Nothing AndAlso Page.FirstEdit.User IsNot User.Me Then

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

    End Class

    Class AfdRequest : Inherits XfdRequest

        'Nominate an article for deletion

        Public Category As String
        Private Subpage As String

        Public Sub Start()
            LogProgress("Tagging '" & Page.Name & "' for deletion discussion...")
            LogPath = Config.AfdLocation & "/Log/" & LogDate() & "#" & Page.Name

            Dim RequestThread As New Thread(AddressOf TagPage)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub TagPage()
            Dim Data As EditData = GetEditData(Page)

            If Data.Error Then
                Callback(AddressOf TagPageFailed)
                Exit Sub

            ElseIf Data.Creating Then
                Callback(AddressOf PageDeleted)
                Exit Sub

            ElseIf Data.Text.ToLower.StartsWith("#redirect [[") Then
                Callback(AddressOf RfdNeeded, CObj(Data))
                Exit Sub

            ElseIf Data.Text.Contains("{{AfDM|") Then
                Callback(AddressOf AlreadyTagged)
                Exit Sub
            End If

            Subpage = GetNominationSubpage(Page.Name, Config.AfdLocation)

            'Tag page
            Data.Text = "{{subst:afd|" & Subpage & "}}" & LF & Data.Text
            Data.Minor = Config.MinorTags
            Data.Watch = Config.WatchTags
            Data.Summary = Config.XfdSummary.Replace("$1", Config.AfdLocation & "/" & Subpage)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf TagPageFailed) Else Callback(AddressOf TagPageDone)
        End Sub

        Private Sub TagPageDone()
            LogProgress("Creating deletion discussion subpage for '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf CreateSubpage)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub TagPageFailed()
            Log("Failed to tag '" & Page.Name & "' for deletion discussion")
            Fail()
        End Sub

        Private Sub CreateSubpage()
            Dim Data As EditData = GetEditData(Config.AfdLocation & "/" & Subpage)

            If Data.Error Then
                Callback(AddressOf CreateSubpageFailed)
                Exit Sub
            End If

            Data.Text = "{{subst:afd2|pg=" & Page.Name & "|cat=" & Category & "|text=" & Reason & "}} ~~~~"
            Data.Minor = Config.MinorOther
            Data.Watch = Config.WatchOther
            Data.Summary = Config.XfdDiscussionSummary.Replace("$1", Page.Name)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf CreateSubpageFailed) Else Callback(AddressOf CreateSubpageDone)
        End Sub

        Private Sub CreateSubpageDone()
            LogProgress("Updating AfD log for nomination of '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf UpdateLog)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub CreateSubpageFailed()
            Log("Failed to create deletion discussion subpage for '" & Page.Name & "'")
            Fail()
        End Sub

        Private Sub UpdateLog()
            Dim Data As EditData = GetEditData(Config.AfdLocation & "/Log/" & LogDate())

            If Data.Error Then
                Callback(AddressOf UpdateLogFailed)
                Exit Sub
            End If

            If Data.Text.Contains("{{" & Config.AfdLocation) Then
                Data.Text = Data.Text.Substring(0, Data.Text.IndexOf("{{" & Config.AfdLocation)) & _
                    "{{" & Config.AfdLocation & "/" & Subpage & "}}" & LF & _
                    Data.Text.Substring(Data.Text.IndexOf("{{" & Config.AfdLocation))
            Else
                Data.Text &= LF & "{{" & Config.AfdLocation & "/" & Subpage & "}}"
            End If

            Data.Minor = Config.MinorOther
            Data.Watch = Config.WatchOther
            Data.Summary = Config.XfdLogSummary.Replace("$1", Config.AfdLocation & "/" & Subpage)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf UpdateLogFailed) Else Callback(AddressOf UpdateLogDone)
        End Sub

        Private Sub UpdateLogDone()
            If Notify Then
                If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing Then
                    DoNotify()
                Else
                    Dim NewHistoryRequest As New HistoryRequest
                    NewHistoryRequest.Page = Page
                    NewHistoryRequest.Start(AddressOf DoNotify)
                End If
            End If

            Complete()
        End Sub

        Private Sub UpdateLogFailed()
            Log("Failed to update AfD log for nomination of '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

    Class CfdRequest : Inherits XfdRequest

        'Nominate a category for deletion

        Public Sub Start()
            LogProgress("Tagging '" & Page.Name & "' for deletion discussion...")
            LogPath = Config.CfdLocation & "/Log/" & LogDate() & "#" & Page.Name

            Dim RequestThread As New Thread(AddressOf TagPage)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub TagPage()
            Dim Data As EditData = GetEditData(Page)

            If Data.Error Then
                Callback(AddressOf TagPageFailed)
                Exit Sub

            ElseIf Data.Creating Then
                Callback(AddressOf PageDeleted)
                Exit Sub

            ElseIf Data.Text.ToLower.StartsWith("#redirect [[") Then
                Callback(AddressOf RfdNeeded, CObj(Data))
                Exit Sub

            ElseIf Data.Text.Contains("<!--BEGIN CFD TEMPLATE-->") Then
                Callback(AddressOf AlreadyTagged)
                Exit Sub
            End If

            Data.Text = "{{subst:cfd}}" & LF & Data.Text
            Data.Minor = Config.MinorTags
            Data.Watch = Config.WatchTags
            Data.Summary = Config.XfdSummary.Replace("$1", LogPath)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf TagPageFailed) Else Callback(AddressOf TagPageDone)
        End Sub

        Private Sub TagPageDone()
            LogProgress("Creating deletion discussion section for '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf CreateDiscussion)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub TagPageFailed()
            Log("Failed to tag '" & Page.Name & "' for deletion discussion")
            Fail()
        End Sub

        Private Sub CreateDiscussion()
            Dim Data As EditData = GetEditData(Config.CfdLocation & "/Log/" & LogDate(), Section:=2)

            If Data.Error Then
                Callback(AddressOf CreateDiscussionFailed)
                Exit Sub
            End If

            Data.Text &= LF & "{{subst:cfd2|" & Page.Name.Substring(Page.Name.IndexOf(":") + 1) & _
                "|text=" & Reason & " ~~~~}}"
            Data.Minor = Config.MinorOther
            Data.Watch = Config.WatchOther
            Data.Summary = Config.XfdLogSummary.Replace("$1", Page.Name)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf CreateDiscussionFailed) Else Callback(AddressOf CreateDiscussionDone)
        End Sub

        Private Sub CreateDiscussionDone()
            If Notify Then
                If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing Then
                    DoNotify()
                Else
                    Dim NewHistoryRequest As New HistoryRequest
                    NewHistoryRequest.Page = Page
                    NewHistoryRequest.Start(AddressOf DoNotify)
                End If
            End If

            Complete()
        End Sub

        Private Sub CreateDiscussionFailed()
            Log("Failed to create deletion discussion section for '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

    Class MfdRequest : Inherits XfdRequest

        'Nominate a page for deletion

        Private Subpage As String

        Public Sub Start()
            LogProgress("Tagging '" & Page.Name & "' for deletion discussion...")
            LogPath = Config.MfdLocation & "#" & Page.Name

            Dim RequestThread As New Thread(AddressOf TagPage)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub TagPage()
            Dim Data As EditData = GetEditData(Page)

            If Data.Error Then
                Callback(AddressOf TagPageFailed)
                Exit Sub

            ElseIf Data.Creating Then
                Callback(AddressOf PageDeleted)
                Exit Sub

            ElseIf Data.Text.ToLower.StartsWith("#redirect") Then
                Callback(AddressOf RfdNeeded, CObj(Data))
                Exit Sub

            ElseIf Data.Text.Contains("{{mfdtag|") Then
                Callback(AddressOf AlreadyTagged)
                Exit Sub
            End If

            Subpage = GetNominationSubpage(Page.Name, Config.MfdLocation)

            'Tag page
            Data.Text = "{{subst:mfd|" & Subpage & "}}" & LF & Data.Text
            Data.Minor = Config.MinorTags
            Data.Watch = Config.WatchTags
            Data.Summary = Config.XfdSummary.Replace("$1", Config.MfdLocation & "/" & Subpage)

            'Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf TagPageFailed) Else Callback(AddressOf TagPageDone)
        End Sub

        Private Sub TagPageDone()
            LogProgress("Creating deletion discussion subpage for '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf CreateSubpage)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub TagPageFailed()
            Log("Failed to tag '" & Page.Name & "' for deletion discussion")
            Fail()
        End Sub

        Private Sub CreateSubpage()
            Dim Data As EditData = GetEditData(GetPage(Config.MfdLocation & "/" & Subpage))

            If Data.Error Then
                Callback(AddressOf CreateSubpageFailed)
                Exit Sub
            End If

            Data.Text = "{{subst:mfd2|pg=" & Page.Name & "|text=" & Reason & "}} ~~~~"
            Data.Minor = Config.MinorOther
            Data.Watch = Config.WatchOther
            Data.Summary = Config.XfdDiscussionSummary.Replace("$1", Page.Name)

            'Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf CreateSubpageFailed) Else Callback(AddressOf CreateSubpageDone)
        End Sub

        Private Sub CreateSubpageDone()
            LogProgress("Updating MfD log for nomination of '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf UpdateLog)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub CreateSubpageFailed()
            Log("Failed to create deletion discussion subpage for '" & Page.Name & "'")
            Fail()
        End Sub

        Private Sub UpdateLog()
            Dim Data As EditData = GetEditData(Config.MfdLocation, , 1)

            If Data.Error Then
                Callback(AddressOf UpdateLogFailed)
                Exit Sub
            End If

            'Add day header if necessary
            Dim DayHeader As String = "===[[" & CStr(Date.UtcNow.Year) & "-" & _
                CStr(Date.UtcNow.Month).PadLeft(2, "0"c) & "-" & CStr(Date.UtcNow.Day).PadLeft(2, "0"c) & "]]==="

            Data.Text = Data.Text.Replace("<!-- " & DayHeader & " -->", LF & DayHeader)
            Data.Text = Data.Text.Replace("<!--" & DayHeader & "-->", LF & DayHeader)

            If Data.Text.Contains(DayHeader) Then
                Data.Text = Data.Text.Substring(0, Data.Text.IndexOf(DayHeader) + 20) & LF & "{{" & _
                    Config.MfdLocation & "/" & Subpage & "}}" & _
                    Data.Text.Substring(Data.Text.IndexOf(DayHeader) + 20)

            ElseIf Data.Text.Contains("===") Then
                Data.Text = Data.Text.Substring(0, Data.Text.IndexOf("===")) & DayHeader & LF & "{{" & _
                    Config.MfdLocation & "/" & Subpage & "}}" & LF & _
                    Data.Text.Substring(Data.Text.IndexOf("==="))

            Else
                Data.Text &= LF & "{{" & Config.MfdLocation & "/" & Subpage & "}}"
            End If

            Data.Minor = Config.MinorOther
            Data.Watch = Config.WatchOther
            Data.Summary = Config.XfdLogSummary.Replace("$1", Config.MfdLocation & "/" & Subpage)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf UpdateLogFailed) Else Callback(AddressOf UpdateLogDone)
        End Sub

        Private Sub UpdateLogDone()
            Complete()

            If Notify Then
                If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing Then
                    DoNotify()
                Else
                    Dim NewHistoryRequest As New HistoryRequest
                    NewHistoryRequest.Page = Page
                    NewHistoryRequest.Start(AddressOf DoNotify)
                End If
            End If
        End Sub

        Private Sub UpdateLogFailed()
            Log("Failed to update MfD log for nomination of '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

    Class IfdRequest : Inherits XfdRequest

        'Nominate an image for deletion

        Public Sub Start()
            LogProgress("Tagging '" & Page.Name & "' for deletion discussion...")
            LogPath = Config.IfdLocation & "/" & LogDate() & "#" & Page.Name

            Dim RequestThread As New Thread(AddressOf TagPage)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub TagPage()
            Dim Data As EditData = GetEditData(Page)

            If Data.Error Then
                Callback(AddressOf TagPageFailed)
                Exit Sub

            ElseIf Data.Creating Then
                Callback(AddressOf PageDeleted)
                Exit Sub

            ElseIf Data.Text.ToLower.StartsWith("#redirect [[") Then
                Callback(AddressOf RfdNeeded, CObj(Data))
                Exit Sub

            ElseIf Data.Text.Contains("{{IfD doc}}") Then
                Callback(AddressOf AlreadyTagged)
                Exit Sub
            End If

            Data.Text = "{{subst:ifd|log=" & LogDate() & "}}" & LF & Data.Text
            Data.Minor = Config.MinorTags
            Data.Watch = Config.WatchTags
            Data.Summary = Config.XfdSummary.Replace("$1", LogPath)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf TagPageFailed) Else Callback(AddressOf TagPageDone)
        End Sub

        Private Sub TagPageDone()
            If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing Then
                LogProgress("Creating deletion discussion section for '" & Page.Name & "'...")

                Dim RequestThread As New Thread(AddressOf CreateDiscussion)
                RequestThread.IsBackground = True
                RequestThread.Start()
            Else
                Dim NewHistoryRequest As New HistoryRequest
                NewHistoryRequest.Page = Page
                NewHistoryRequest.Start(AddressOf GotHistory)
            End If
        End Sub

        Private Sub GotHistory(ByVal Result As Request.Output)
            If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing Then
                LogProgress("Creating deletion discussion section for '" & Page.Name & "'...")

                Dim RequestThread As New Thread(AddressOf CreateDiscussion)
                RequestThread.IsBackground = True
                RequestThread.Start()
            Else
                Callback(AddressOf CreateDiscussionFailed)
            End If
        End Sub

        Private Sub TagPageFailed()
            Log("Failed to tag '" & Page.Name & "' for deletion discussion")
            Fail()
        End Sub

        Private Sub CreateDiscussion()
            Dim Data As EditData = GetEditData(Config.IfdLocation & "/" & LogDate())

            If Data.Error Then
                Callback(AddressOf CreateDiscussionFailed)
                Exit Sub
            End If

            Data.Text &= LF & "{{subst:ifd2|" & Page.Name.Substring(Page.Name.IndexOf(":") + 1) & _
                "|uploader=" & Page.FirstEdit.User.Name & "|reason=" & Reason & "}} ~~~~"
            Data.Minor = Config.MinorOther
            Data.Watch = Config.WatchOther
            Data.Summary = Config.XfdLogSummary.Replace("$1", Page.Name)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf CreateDiscussionFailed) Else Callback(AddressOf CreateDiscussionDone)
        End Sub

        Private Sub CreateDiscussionDone()
            If Notify Then DoNotify()
            Complete()
        End Sub

        Private Sub CreateDiscussionFailed()
            Log("Failed to create deletion discussion section for '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

    Class TfdRequest : Inherits XfdRequest

        'Nominate a template for deletion

        Public Sub Start()
            LogProgress("Tagging '" & Page.Name & "' for deletion discussion...")
            LogPath = Config.TfdLocation & "/Log/" & LogDate() & "#" & Page.Name

            Dim RequestThread As New Thread(AddressOf TagPage)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub TagPage()
            Dim Data As EditData = GetEditData(Page)

            If Data.Error Then
                Callback(AddressOf TagPageFailed)
                Exit Sub

            ElseIf Data.Creating Then
                Callback(AddressOf PageDeleted)
                Exit Sub

            ElseIf Data.Text.ToLower.StartsWith("#redirect [[") Then
                Callback(AddressOf RfdNeeded, CObj(Data))
                Exit Sub

            ElseIf Data.Text.Contains("{{tfd|") Then
                Callback(AddressOf AlreadyTagged)
                Exit Sub
            End If

            Data.Text = "<noinclude>{{tfd|" & Page.Name & "}}</noinclude>" & LF & Data.Text
            Data.Minor = Config.MinorTags
            Data.Watch = Config.WatchTags
            Data.Summary = Config.XfdSummary.Replace("$1", LogPath)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf TagPageFailed) Else Callback(AddressOf TagPageDone)
        End Sub

        Private Sub TagPageDone()
            LogProgress("Creating deletion discussion section for '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf CreateDiscussion)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub TagPageFailed()
            Log("Failed to tag '" & Page.Name & "' for deletion discussion")
            Fail()
        End Sub

        Private Sub CreateDiscussion()
            Dim Data As EditData = GetEditData(Config.CfdLocation & "/Log/" & LogDate(), Section:=1)

            If Data.Error Then
                Callback(AddressOf CreateDiscussionFailed)
                Exit Sub
            End If

            If Data.Text.Contains("====") Then
                Data.Text = Data.Text.Substring(0, Data.Text.IndexOf("====")) & "{{subst:tfd2|" & Page.Name & _
                    "|text=" & Reason & " ~~~~}}" & LF & LF & Data.Text.Substring(Data.Text.IndexOf("===="))
            Else
                Data.Text &= LF & "{{subst:tfd2|" & Page.Name & "|text=" & Reason & " ~~~~}}"
            End If

            Data.Minor = Config.MinorOther
            Data.Watch = Config.WatchOther
            Data.Summary = Config.XfdLogSummary.Replace("$1", Page.Name)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf CreateDiscussionFailed) Else Callback(AddressOf CreateDiscussionDone)
        End Sub

        Private Sub CreateDiscussionDone()
            Complete()

            If Notify Then
                If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing Then
                    DoNotify()
                Else
                    Dim NewHistoryRequest As New HistoryRequest
                    NewHistoryRequest.Page = Page
                    NewHistoryRequest.Start(AddressOf DoNotify)
                End If
            End If
        End Sub

        Private Sub CreateDiscussionFailed()
            Log("Failed to create deletion discussion section for '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

    Class RfdRequest : Inherits XfdRequest

        'Nominate a redirect for deletion

        Public Data As EditData
        Private Target As String

        Public Sub Start()
            If Config.RfdLocation IsNot Nothing Then
                LogProgress("Tagging '" & Page.Name & "' for deletion discussion...")

                Dim RequestThread As New Thread(AddressOf TagPage)
                RequestThread.IsBackground = True
                RequestThread.Start()
            Else
                Log("Did not tag '" & Page.Name & _
                    "' for deletion discussion, as it is a redirect and no redirect discussion process is defined")
                Fail()
            End If
        End Sub

        Private Sub TagPage()
            If Not (Data.Text.Contains("[[") AndAlso Data.Text.Contains("]]")) Then
                Callback(AddressOf TagPageFailed)
                Exit Sub

            ElseIf Data.Text.Contains("{{rfd}}") Then
                Callback(AddressOf AlreadyTagged)
                Exit Sub
            End If

            Target = Data.Text.Substring(Data.Text.IndexOf("[[") + 2)
            If Target.Contains("]]") Then Target = Target.Substring(0, Target.IndexOf("]]"))

            Data.Text = "{{rfd}}" & LF & Data.Text
            Data.Minor = Config.MinorTags
            Data.Watch = Config.WatchTags
            Data.Summary = Config.XfdSummary.Replace("$1", Config.RfdLocation & "/Log/" & LogDate() & "#" & _
                Page.Name & " .E2.86.92 " & Target)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf TagPageFailed) Else Callback(AddressOf TagPageDone)
        End Sub

        Private Sub TagPageDone()
            LogProgress("Creating deletion discussion section for '" & Page.Name & "'...")
            LogPath = Config.RfdLocation & "/Log/" & LogDate() & "#" & Page.Name

            Dim RequestThread As New Thread(AddressOf CreateDiscussion)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub TagPageFailed()
            Log("Failed to tag '" & Page.Name & "' for deletion discussion")
            Fail()
        End Sub

        Private Sub CreateDiscussion()
            Dim Data As EditData = GetEditData(Config.RfdLocation & "/Log/" & LogDate())

            If Data.Error Then
                Callback(AddressOf CreateDiscussionFailed)
                Exit Sub
            End If

            If Data.Text.Contains("====") Then
                Data.Text = Data.Text.Substring(0, Data.Text.IndexOf("====")) & _
                    "{{subst:rfd2|redirect=" & Page.Name & "|target=" & Target & "|text=" & Reason & "}} ~~~~" & _
                    LF & LF & Data.Text.Substring(Data.Text.IndexOf("===="))
            Else
                Data.Text &= LF & "{{subst:rfd2|redirect=" & Page.Name & "|target=" & Target & "|text=" & _
                    Reason & "}} ~~~~"
            End If

            Data.Minor = Config.MinorOther
            Data.Watch = Config.WatchOther
            Data.Summary = Config.XfdLogSummary.Replace("$1", Page.Name)

            Data = PostEdit(Data)

            If Data.Error Then Callback(AddressOf CreateDiscussionFailed) Else Callback(AddressOf CreateDiscussionDone)
        End Sub

        Private Sub CreateDiscussionDone()
            Complete()

            If Notify Then
                If Page.FirstEdit IsNot Nothing AndAlso Page.FirstEdit.User IsNot Nothing Then
                    DoNotify()
                Else
                    Dim NewHistoryRequest As New HistoryRequest
                    NewHistoryRequest.Page = Page
                    NewHistoryRequest.Start(AddressOf DoNotify)
                End If
            End If
        End Sub

        Private Sub CreateDiscussionFailed()
            Log("Failed to create deletion discussion section for '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

End Namespace
