Imports System.IO
Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class BlockRequest : Inherits Request

        'Block a user
        Public User As User, Summary, Expiry, NotifyTemplate As String
        Public AnonOnly, Autoblock, BlockEmail, BlockCreation, BlockTalkEdit, Notify As Boolean

        Protected Overrides Sub Process()
            LogProgress(Msg("block-progress", User.Name))

            Dim Result As ApiResult

            'Check for range block already affecting IP address
            If User.Anonymous Then
                Result = DoApiRequest("action=query&list=blocks&bkip=" & UrlEncode(User.Name))

                If Result.Error Then
                    Fail(Msg("block-fail", User.Name), Result.ErrorMessage)
                    Exit Sub
                End If

                If Result.Text.Contains("<blocks>") Then
                    For Each Item As String In Split(FindString(Result.Text, "<blocks>", "</blocks>"), "<block ")

                        Dim BlockedUser As String = GetParameter(Item, "user")

                        If BlockedUser.Contains("/") Then
                            If MessageBox.Show(Msg("block-rangeblockwarning", User.Name, BlockedUser), _
                                "Huggle", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, _
                                MessageBoxDefaultButton.Button2) = DialogResult.No Then

                                Cancel()
                                Exit Sub
                            End If

                            Exit For
                        End If
                    Next Item
                End If
            End If

            'Get token
            Result = DoApiRequest("action=query&prop=info&titles=-&intoken=block")

            If Result.Error Then
                Fail(Msg("block-fail", User.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Dim Token As String = GetParameter(Result.Text, "blocktoken")

            'Block user
            Dim PostString As String = "user=" & UrlEncode(User.Name) & _
                "&reason=" & UrlEncode(Summary) & "&expiry=" & UrlEncode(Expiry) & "&token=" & UrlEncode(Token)

            If BlockCreation Then PostString &= "&nocreate"
            If BlockEmail Then PostString &= "&noemail"
            If Autoblock Then PostString &= "&autoblock"
            If AnonOnly Then PostString &= "&anononly"

            'API defaults to NOT allowing user to edit talk page, even though this is usually not what is wanted
            If Not BlockTalkEdit Then PostString &= "&allowusertalk"

            Result = DoApiRequest("action=block", PostString)

            If Result.Error _
                Then Fail(Msg("block-fail", User.Name), Result.ErrorMessage) _
                Else Complete(Msg("block-done", User.Name))
        End Sub

        Protected Overrides Sub Done()
            'Notify user of block
            If State = States.Complete AndAlso Notify Then
                Dim NewRequest As New BlockNotificationRequest

                NewRequest.User = User
                NewRequest.Expiry = Expiry
                NewRequest.Reason = Summary
                NewRequest.Template = NotifyTemplate
                NewRequest.Start()
            End If
        End Sub

    End Class

    Class DeleteRequest : Inherits Request

        'Delete a page

        Public Page As Page, Summary As String, Watch As Boolean

        Protected Overrides Sub Process()
            LogProgress(Msg("delete-progress", Page.Name))

            'Get token
            Dim Result As ApiResult = DoApiRequest("action=query&prop=info&intoken=delete&titles=" & UrlEncode(Page.Name))

            If Result.Error Then
                Fail(Msg("delete-fail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Dim Token As String = GetParameter(Result.Text, "deletetoken")

            'Delete the page
            Dim PostString As String = "title=" & UrlEncode(Page.Name) & _
                "&reason=" & UrlEncode(Summary) & "&token=" & UrlEncode(Token)

            If Watch Then PostString &= "&watch"

            Result = DoApiRequest("action=delete", PostString)

            If Result.Error _
                Then Fail(Msg("delete-fail", Page.Name), Result.ErrorMessage) _
                Else Complete(Msg("delete-done", Page.Name))
        End Sub

    End Class

    Class EmailRequest : Inherits Request

        'E-mail a user

        Public User As User, Subject As String = Config.EmailSubject, Message As String, CcMe As Boolean

        Protected Overrides Sub Process()
            LogProgress(Msg("email-progress", User.Name))

            'Get token
            Dim Result As ApiResult = DoApiRequest("action=query&prop=info&titles=-&intoken=email")

            If Result.Error Then
                Fail(Msg("email-fail", User.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Dim Token As String = GetParameter(Result.Text, "emailtoken")

            'Send e-mail
            Dim PostString As String = "target=" & UrlEncode(User.Name) & _
                "&subject=" & UrlEncode(Subject) & "&text=" & Message & "&token=" & UrlEncode(Token)

            If CcMe Then PostString &= "&ccme"

            Result = DoApiRequest("action=emailuser", PostString)

            If Result.Error _
                Then Fail(Msg("email-fail", User.Name), Result.ErrorMessage) _
                Else Complete(Msg("email-done", User.Name))
        End Sub

    End Class

    Class MoveRequest : Inherits Request

        'Move a page

        Public Page As Page, Target, Summary As String, MoveTalk As Boolean

        Protected Overrides Sub Process()
            LogProgress(Msg("move-progress", Page.Name, Target))

            'Get token
            Dim Result As ApiResult = DoApiRequest("action=query&prop=info&intoken=move&titles=" & UrlEncode(Page.Name))

            If Result.Error Then
                Fail(Msg("move-fail", Page.Name, Target), Result.ErrorMessage)
                Exit Sub
            End If

            Dim Token As String = GetParameter(Result.Text, "movetoken")

            'Move the page
            Dim PostString As String = "from=" & UrlEncode(Page.Name) & "&to=" & UrlEncode(Target) & _
                "&reason=" & UrlEncode(Summary) & "&token=" & UrlEncode(Token)

            If MoveTalk Then PostString &= "&movetalk"

            Result = DoApiRequest("action=move", PostString)

            If Result.Error _
                Then Fail(Msg("move-fail", Page.Name, Target), Result.ErrorMessage) _
                Else Complete(Msg("move-done", Page.Name, Target))
        End Sub

    End Class

    Class PatrolRequest : Inherits Request

        'Mark a page as patrolled

        Public Page As Page

        Protected Overrides Sub Process()
            LogProgress(Msg("patrol-progress", Page.Name))

            'New page patrolling is an awkward hack of an extension and not part of MediaWiki
            'Only recent-changes entries, not pages, are associated with a patrol token
            'making it difficult to mark a given page as patrolled, especially if it's old

            If Page.Patrolled Then
                Page.Patrolled = True
                Fail(Msg("patrol-fail", Page.Name), Msg("patrol-alreadydone"))
                Exit Sub
            End If

            Dim Result As ApiResult

            If PatrolToken Is Nothing OrElse Page.Rcid Is Nothing Then
                'Find rcid and/or patrol token
                Result = DoApiRequest("&rclimit=500&action=query&list=recentchanges&rctype=new&rctoken=patrol")

                If Result.Error Then
                    Fail(Msg("patrol-fail", Page.Name), Result.ErrorMessage)
                    Exit Sub
                End If

                PatrolToken = GetParameter(Result.Text, "patroltoken")

                For Each Item As String In Split(FindString _
                    (Result.Text, "<recentchanges>", "</recentchanges>"), "<rc ")

                    GetPage(GetParameter(Item, "title")).Rcid = GetParameter(Item, "rcid")
                Next Item
            End If

            If Page.Rcid Is Nothing Then
                Fail(Msg("patrol-fail", Page.Name), Msg("patrol-notfound"))
                Exit Sub
            End If

            'Patrol the page
            Result = DoApiRequest("action=patrol&rcid=" & Page.Rcid & "&token=" & UrlEncode(PatrolToken))

            If Result.Error _
                Then Fail(Msg("patrol-fail", Page.Name), Result.ErrorMessage) _
                Else Complete(Msg("patrol-done", Page.Name))
        End Sub

    End Class

    Class ProtectRequest : Inherits Request

        'Protect a page

        Public Page As Page, EditLevel, MoveLevel, Expiry, Summary As String, Cascade As Boolean

        Protected Overrides Sub Process()
            LogProgress(Msg("protect-progress", Page.Name))

            'Get token
            Dim Result As ApiResult = DoApiRequest("action=query&prop=info&intoken=protect&titles=" & UrlEncode(Page.Name))

            If Result.Error Then
                Fail(Msg("protect-fail", Page.Name), Result.ErrorMessage)
                Exit Sub
            End If

            Dim Token As String = GetParameter(Result.Text, "protecttoken")

            'Protect the page
            Dim PostString As String = "title=" & UrlEncode(Page.Name) & _
                "&reason=" & UrlEncode(Summary) & "protections=edit:" & EditLevel & "|move:" & MoveLevel & _
                "expiry=" & Expiry & "&token=" & UrlEncode(Token)

            If Cascade Then PostString &= "&cascade"

            Result = DoApiRequest("action=protect", PostString)

            If Result.Error _
                Then Fail(Msg("protect-fail", Page.Name), Result.ErrorMessage) _
                Else Complete(Msg("protect-done", Page.Name))
        End Sub

    End Class

    Class UnwatchRequest : Inherits Request

        'Removes page from user's watchlist

        Public Page As Page, Manual As Boolean

        Protected Overrides Sub Process()
            If Manual Then LogProgress(Msg("unwatch-progress", Page.Name))

            Dim Result As ApiResult = DoApiRequest("action=watch&unwatch&title=" & UrlEncode(Page.Name))

            If Result IsNot Nothing Then
                If Watchlist.Contains(Page.SubjectPage) Then Watchlist.Remove(Page.SubjectPage)
                If Manual Then Complete(Msg("unwatch-done", Page.Name)) Else Complete()
            Else
                Fail(Msg("unwatch-fail", Page.Name), Result.ErrorMessage)
            End If
        End Sub

        Protected Overrides Sub Done()
            If CurrentEdit IsNot Nothing AndAlso Page Is CurrentEdit.Page Then MainForm.UpdateWatchButton()
        End Sub

    End Class

    Class UpdateRequest : Inherits Request

        'Download latest version of the application

        Public Filename As String, ProgressBar As ProgressBar

        Private Progress, Total As Integer

        Protected Overrides Sub Process()
            'Download into memory and then write to file
            Try
                Dim Request As HttpWebRequest = CType(WebRequest.Create(Config.DownloadLocation.Replace("$1", _
                    VersionString(Config.LatestVersion))), HttpWebRequest)
                Request.Proxy = Proxy
                Request.UserAgent = Config.UserAgent

                Dim Response As WebResponse = Request.GetResponse
                Dim ResponseStream As Stream = Response.GetResponseStream
                Total = CInt(Response.ContentLength)
                Dim MemoryStream As New MemoryStream(Total)

                Dim Buffer(255) As Byte, S As Integer

                Do
                    S = ResponseStream.Read(Buffer, 0, Buffer.Length)
                    MemoryStream.Write(Buffer, 0, S)
                    Progress += S
                    ControlInvoke(ProgressBar, AddressOf UpdateProgress)
                Loop While S > 0

                File.WriteAllBytes(Filename, MemoryStream.ToArray)

            Catch ex As Exception
                If State <> States.Cancelled Then Fail(Msg("update-error"), ex.Message)
            End Try

            If State = States.Cancelled Then
                If File.Exists(Filename) Then File.Delete(Filename)
                Thread.CurrentThread.Abort()
                Exit Sub
            End If

            Complete()
        End Sub

        Private Sub UpdateProgress()
            If ProgressBar IsNot Nothing Then
                ProgressBar.Maximum = Total
                ProgressBar.Value = Progress
            End If
        End Sub

    End Class

    Class WatchRequest : Inherits Request

        'Adds page to your watchlist

        Public Page As Page, Manual As Boolean

        Protected Overrides Sub Process()
            If Manual Then LogProgress(Msg("watch-progress", Page.Name))

            Dim Result As ApiResult = DoApiRequest("action=watch&title=" & UrlEncode(Page.Name))

            If Result IsNot Nothing Then
                If Not Watchlist.Contains(Page.SubjectPage) Then Watchlist.Add(Page.SubjectPage)
                If Manual Then Complete(Msg("watch-done", Page.Name)) Else Complete()
            Else
                Fail(Msg("watch-fail", Page.Name), Result.ErrorMessage)
            End If
        End Sub

        Protected Overrides Sub Done()
            If CurrentEdit IsNot Nothing AndAlso Page Is CurrentEdit.Page Then MainForm.UpdateWatchButton()
        End Sub

    End Class

End Namespace