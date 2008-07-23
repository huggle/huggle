Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Module ActionRequests

    Class BlockRequest : Inherits Request

        'Block a user

        Public ThisUser As User, Reason, Expiry, Template As String
        Public AnonOnly, Autoblock, BlockEmail, BlockCreation, Notify As Boolean

        Public Sub Start()
            Log("Blocking '" & ThisUser.Name & "'...", ThisUser, True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            'Check for range block already affecting IP address
            If ThisUser.Anonymous Then
                Dim Rangeblocks As String = _
                    GetText(SitePath & "w/api.php?format=xml&action=query&list=blocks&bkip=" & ThisUser.Name)

                If Rangeblocks.Contains("<blocks>") Then
                    Rangeblocks = Rangeblocks.Substring(Rangeblocks.IndexOf("<blocks>") + 8)
                    Rangeblocks = Rangeblocks.Substring(0, Rangeblocks.IndexOf("</blocks>"))

                    For Each Item As String In Rangeblocks.Split(New String() {"<block "}, StringSplitOptions.RemoveEmptyEntries)
                        Dim User As String = Item.Substring(Item.IndexOf("user=""") + 6)
                        User = User.Substring(0, User.IndexOf(""""))
                        User = HtmlDecode(User)

                        If User.Contains("/") Then
                            If MsgBox(ThisUser.Name & " is already affected by a rangeblock on " & User & _
                                "." & vbCrLf & "This block will override the effect of the rangeblock. Continue?", _
                                MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton2, _
                                "Block " & ThisUser.Name) = MsgBoxResult.No Then

                                Callback(AddressOf Failed)
                                Exit Sub
                            End If

                            Exit For
                        End If
                    Next Item
                End If
            End If

            Dim Client As New WebClient, Retries As Integer = 3, Result As String = ""
            Dim EditTokenMatch As Match

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Proxy = Login.Proxy

                Retries -= 1
                Try
                    Result = UTF8.GetString(Client.DownloadData(SitePath & "w/index.php?title=Special:Blockip/" _
                        & UrlEncode(ThisUser.Name)))

                Catch ex As WebException
                End Try

                EditTokenMatch = Regex.Match(Result, _
                    "<input name=""wpEditToken"" type=""hidden"" value=""(.*?)"" />", RegexOptions.Compiled)

            Loop Until EditTokenMatch.Success OrElse Retries = 0

            Dim EditToken As String = EditTokenMatch.Groups(1).Value

            If Retries = 0 Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Client.Proxy = Login.Proxy

                Retries -= 1

                Dim PostString As String = "wpBlockAddress=" & UrlEncode(ThisUser.Name) & _
                    "&wpBlockReasonList=other" & "&wpBlockReason=" & UrlEncode(Reason) & _
                    "&wpBlockExpiry=" & UrlEncode(Expiry) & "&wpEditToken=" & UrlEncode(EditToken)

                If BlockCreation Then PostString &= "&wpCreateAccount=1"
                If BlockEmail Then PostString &= "&wpEmailBan=1"
                If Autoblock Then PostString &= "&wpEnableAutoblock=1"
                If AnonOnly Then PostString &= "&wpAnonOnly=1"

                If Cancelled Then Exit Sub
                Try
                    Result = UTF8.GetString(Client.UploadData(SitePath & _
                        "w/index.php?title=Special:Blockip&action=submit", UTF8.GetBytes(PostString)))
                Catch ex As Exception
                End Try

            Loop Until IsWikiPage(Result) OrElse Retries = 0

            If Retries > 0 Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done(ByVal O As Object)
            If Notify Then
                Dim NewRequest As New BlockNotificationRequest

                NewRequest.ThisUser = ThisUser
                NewRequest.Expiry = Expiry
                NewRequest.Reason = Reason
                NewRequest.Template = Template
                NewRequest.Start()
            End If

            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            Delog(ThisUser)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Log("Failed to block '" & ThisUser.Name & "'")
            If CurrentEdit.User Is ThisUser Then Main.UserReportB.Enabled = True

            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            Delog(ThisUser)
        End Sub

    End Class

    Class PatrolRequest : Inherits Request

        'Mark a page as patrolled

        Public Page As Page

        Public Sub Start()
            Log("Marking '" & Page.Name & "' as patrolled...", Page, True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            'New page patrolling is an awkward hack of an extension and not part of MediaWiki
            'Viewing a page doesn't give a patrol link, you have to go to Special:Newpages
            'making it very difficult to mark a given page as patrolled, especially if it's old

            If Page.Patrolled Then
                Callback(AddressOf AlreadyPatrolled)
                Exit Sub
            End If

            If Page.Rcid Is Nothing Then
                Dim Result As String = GetText(SitePath & "w/index.php?title=Special:Newpages&namespace=all&limit=500")

                If Result Is Nothing Then
                    Callback(AddressOf Failed)
                    Exit Sub
                End If

                Result = Result.Substring(Result.IndexOf("<ul>"))
                Result = Result.Substring(0, Result.IndexOf("</ul>"))

                For Each Item As String In Result.Split(New String() {"</li>"}, StringSplitOptions.None)
                    If Item.Contains("<a") Then
                        Dim Url As String = Item.Substring(Item.IndexOf("<a") + 10)
                        Url = HtmlDecode(Url.Substring(0, Url.IndexOf("""")))

                        Dim PageName As String

                        If Url.Contains("&rcid=") Then
                            Url = Url.Substring(Url.IndexOf("?title=") + 7)
                            PageName = UrlDecode(Url.Substring(0, Url.IndexOf("&rcid="))).Replace("_", " ")
                            GetPage(PageName).Rcid = Url.Substring(Url.IndexOf("&rcid=") + 6)
                            GetPage(PageName).Patrolled = False
                        Else
                            Url = Url.Substring(Url.IndexOf("wiki/") + 5)
                            PageName = UrlDecode(Url).Replace("_", " ")
                            GetPage(PageName).Patrolled = True
                        End If
                    End If
                Next Item
            End If

            If Page.Patrolled Then
                Callback(AddressOf AlreadyPatrolled)
                Exit Sub

            ElseIf Page.Rcid Is Nothing Then
                Callback(AddressOf NotFound)
                Exit Sub
            End If

            If Cancelled Then Exit Sub
            Dim Result2 As String = GetText(SitePath & "w/index.php?title=" & UrlEncode(Page.Name.Replace(" ", "_")) & _
                "&action=markpatrolled&rcid=" & Page.Rcid)

            If Not Result2.Contains("Marked as patrolled</h1>") Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Callback(AddressOf Done)
        End Sub

        Private Sub Done(ByVal O As Object)
            Delog(Page)
            Page.Patrolled = True
            Log("Marked '" & Page.Name & "' as patrolled")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub NotFound(ByVal O As Object)
            Delog(Page)
            Page.Patrolled = True
            Log("Did not mark '" & Page.Name & "' as patrolled; cannot find it in the new page log")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub AlreadyPatrolled(ByVal O As Object)
            Delog(Page)
            Page.Patrolled = True
            Log("'" & Page.Name & "' has already been patrolled")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Delog(Page)
            Log("Failed to mark '" & Page.Name & "' as patrolled")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

    End Class

    Class MoveRequest : Inherits Request

        'Move a page

        Public ThisPage As Page, Target, Reason As String

        Public Sub Start()
            Log("Moving '" & ThisPage.Name & "'...", ThisPage, True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Client As New WebClient, Retries As Integer = 3, Result As String
            Dim EditTokenMatch As Match

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Proxy = Login.Proxy

                Retries -= 1
                Result = UTF8.GetString(Client.DownloadData _
                    (SitePath & "w/index.php?title=Special:Movepage/" & UrlEncode(ThisPage.Name)))

                If Result.Contains("<div class=""permissions-errors"">") Then
                    Callback(AddressOf PermissionDenied)
                    Exit Sub
                End If

                EditTokenMatch = Regex.Match(Result, _
                    "<input name=""wpEditToken"" type=""hidden"" value=""(.*?)"" />", RegexOptions.Compiled)

            Loop Until EditTokenMatch.Success OrElse Retries = 0

            If Retries = 0 Then
                Callback(AddressOf Failed, CObj(Result))
                Exit Sub
            End If

            Dim EditToken As String = EditTokenMatch.Groups(1).Value

            Retries = 3

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Proxy = Login.Proxy

                Retries -= 1

                Dim PostString As String = "wpOldTitle=" & UrlEncode(ThisPage.Name) & _
                    "&wpNewTitle=" & UrlEncode(Target) & "&wpReason=" & UrlEncode(Reason) & _
                    "&wpEditToken=" & UrlEncode(EditToken)

                If Cancelled Then Exit Sub
                Try
                    Result = UTF8.GetString(Client.UploadData(SitePath & _
                        "w/index.php?title=Special:Movepage/" & UrlEncode(ThisPage.Name) & _
                        "&action=submit", UTF8.GetBytes(PostString)))
                Catch ex As Exception
                End Try

            Loop Until IsWikiPage(Result) OrElse Retries = 0

            If Result.Contains("<h1 class=""firstHeading"">Move succeeded</h1>") _
                Then Callback(AddressOf Done, CObj(Result)) Else Callback(AddressOf Failed, CObj(Result))
        End Sub

        Private Sub Done(ByVal O As Object)
            Delog(ThisPage)
            Log("Moved '" & ThisPage.Name & "' to '" & Target & "'")
            Main.PageB.Text = Target
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Delog(ThisPage)
            Log("Did not move '" & ThisPage.Name & "' to '" & Target & "'; the target might already exist")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

        Private Sub PermissionDenied(ByVal O As Object)
            Delog(ThisPage)
            Log("Did not move '" & ThisPage.Name & "' to '" & Target & "' – permission denied.")
            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
        End Sub

    End Class

    Class DeleteRequest : Inherits Request

        'Delete a page

        Public ThisPage As Page, Summary As String

        Public Sub Start()
            Log("Deleting '" & ThisPage.Name & "'...", ThisPage, True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Client As New WebClient, Retries As Integer = 3, Result As String = ""
            Dim EditTokenMatch As Match

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Proxy = Login.Proxy

                Retries -= 1
                Try
                    Result = UTF8.GetString(Client.DownloadData(SitePath & "w/index.php?title=" & _
                        UrlEncode(ThisPage.Name) & "&action=delete"))

                Catch ex As WebException
                End Try

                EditTokenMatch = Regex.Match(Result, _
                    "<input name=""wpEditToken"" type=""hidden"" value=""(.*?)"" />", RegexOptions.Compiled)

            Loop Until EditTokenMatch.Success OrElse Retries = 0

            If Retries = 0 Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            If Summary = "" AndAlso Result.Contains("<input name=""wpReason""") Then
                Summary = Result.Substring(Result.IndexOf("<input name=""wpReason"""))
                Summary = Summary.Substring(Summary.IndexOf(" value=""") + 8)
                Summary = Summary.Substring(0, Summary.IndexOf(""""))
                Summary = HtmlDecode(Summary)
            End If

            Dim EditToken As String = EditTokenMatch.Groups(1).Value

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Client.Proxy = Login.Proxy

                Retries -= 1

                Dim PostString As String = "wpReason=" & UrlEncode(Summary) & "&wpEditToken=" & UrlEncode(EditToken)

                If Cancelled Then Exit Sub
                Try
                    Result = UTF8.GetString(Client.UploadData(SitePath & "w/index.php?title=" & _
                        UrlEncode(ThisPage.Name) & "&action=delete", UTF8.GetBytes(PostString)))

                Catch ex As Exception
                End Try

            Loop Until IsWikiPage(Result) OrElse Retries = 0

            If Retries > 0 Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done(ByVal O As Object)
            Log("Deleted '" & ThisPage.Name & "'")
            ThisPage.DeletesCurrent = False

            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            Delog(ThisPage)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Log("Failed to delete '" & ThisPage.Name & "'")
            If CurrentEdit.Page Is ThisPage Then Main.PageDeleteB.Enabled = True

            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            Delog(ThisPage)
        End Sub

    End Class

    Class ProtectRequest : Inherits Request

        'Protect a page

        Public ThisPage As Page, EditLevel, MoveLevel, Expiry, Summary As String, Cascade As Boolean

        Public Sub Start()
            Log("Protecting '" & ThisPage.Name & "'...", ThisPage, True)
            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Client As New WebClient, Retries As Integer = 3, Result As String = ""
            Dim EditTokenMatch As Match

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Proxy = Login.Proxy

                Retries -= 1
                Try
                    Result = UTF8.GetString(Client.DownloadData(SitePath & "w/index.php?title=" & _
                        UrlEncode(ThisPage.Name) & "&action=protect"))

                Catch ex As WebException
                End Try

                EditTokenMatch = Regex.Match(Result, _
                    "<input type=""hidden"" name=""wpEditToken"" value=""(.*?)"" />", RegexOptions.Compiled)

            Loop Until EditTokenMatch.Success OrElse Retries = 0

            If Retries = 0 Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Dim EditToken As String = EditTokenMatch.Groups(1).Value

            Do
                Client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent)
                Client.Headers.Add(HttpRequestHeader.Cookie, Cookie)
                Client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
                Client.Proxy = Login.Proxy

                Retries -= 1

                Dim PostString As String = "mwProtect-reason=" & UrlEncode(Summary) _
                    & "&wpEditToken=" & UrlEncode(EditToken) & "&mwProtect-level-edit=" & EditLevel _
                    & "&mwProtect-level-move=" & MoveLevel & "&mwProtect-expiry=" & Expiry

                If Cascade Then PostString &= "&mwProtect-cascade=1"

                If Cancelled Then Exit Sub
                Try
                    Result = UTF8.GetString(Client.UploadData(SitePath & "w/index.php?title=" & _
                        UrlEncode(ThisPage.Name) & "&action=protect", UTF8.GetBytes(PostString)))

                Catch ex As Exception
                End Try

            Loop Until IsWikiPage(Result) OrElse Retries = 0

            If Retries > 0 Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done(ByVal O As Object)
            Log("Protected '" & ThisPage.Name & "'")
            ThisPage.ProtectionsCurrent = False

            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            Delog(ThisPage)
        End Sub

        Private Sub Failed(ByVal O As Object)
            Log("Failed to protect '" & ThisPage.Name & "'")

            If PendingRequests.Contains(Me) Then PendingRequests.Remove(Me)
            Delog(ThisPage)
        End Sub

    End Class

End Module