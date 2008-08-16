Imports System.Net
Imports System.Text.Encoding
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    Class BlockRequest : Inherits Request

        'Block a user

        Public User As User, Reason, Expiry, NotifyTemplate As String
        Public AnonOnly, Autoblock, BlockEmail, BlockCreation, Notify As Boolean

        Public Sub Start()
            LogProgress("Blocking '" & User.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            'Check for range block already affecting IP address
            If User.Anonymous Then
                Dim Rangeblocks As String = _
                    GetApi("format=xml&action=query&list=blocks&bkip=" & UrlEncode(User.Name))

                If Rangeblocks.Contains("<blocks>") Then
                    Rangeblocks = Rangeblocks.Substring(Rangeblocks.IndexOf("<blocks>") + 8)
                    Rangeblocks = Rangeblocks.Substring(0, Rangeblocks.IndexOf("</blocks>"))

                    For Each Item As String In Rangeblocks.Split(New String() {"<block "}, StringSplitOptions.RemoveEmptyEntries)
                        Dim BlockedUser As String = Item.Substring(Item.IndexOf("user=""") + 6)
                        BlockedUser = BlockedUser.Substring(0, BlockedUser.IndexOf(""""))
                        BlockedUser = HtmlDecode(BlockedUser)

                        If BlockedUser.Contains("/") Then
                            If MessageBox.Show(User.Name & " is already affected by a rangeblock on " & BlockedUser & _
                                "." & LF & "This block will override the effect of the rangeblock. Continue?", _
                                "Huggle", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, _
                                MessageBoxDefaultButton.Button2) = DialogResult.No Then

                                Callback(AddressOf Failed)
                                Exit Sub
                            End If

                            Exit For
                        End If
                    Next Item
                End If
            End If

            Dim Result As String = GetText("title=Special:Blockip/" & UrlEncode(User.Name))
            Dim EditTokenMatch As Match = Regex.Match(Result, _
                "<input name=""wpEditToken"" type=""hidden"" value=""(.*?)"" />", RegexOptions.Compiled)

            If Not EditTokenMatch.Success Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Dim EditToken As String = EditTokenMatch.Groups(1).Value

            Dim PostString As String = "wpBlockAddress=" & UrlEncode(User.Name) & _
                "&wpBlockReasonList=other" & "&wpBlockReason=" & UrlEncode(Reason) & _
                "&wpBlockExpiry=" & UrlEncode(Expiry) & "&wpEditToken=" & UrlEncode(EditToken)

            If BlockCreation Then PostString &= "&wpCreateAccount=1"
            If BlockEmail Then PostString &= "&wpEmailBan=1"
            If Autoblock Then PostString &= "&wpEnableAutoblock=1"
            If AnonOnly Then PostString &= "&wpAnonOnly=1"

            Result = PostData("title=Special:Blockip&action=submit", PostString)

            If IsWikiPage(Result) Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            If Notify Then
                Dim NewRequest As New BlockNotificationRequest

                NewRequest.User = User
                NewRequest.Expiry = Expiry
                NewRequest.Reason = Reason
                NewRequest.Template = NotifyTemplate
                NewRequest.Start()
            End If

            Complete()
        End Sub

        Private Sub Failed()
            Log("Failed to block '" & User.Name & "'")
            If CurrentEdit.User Is User Then MainForm.UserReportB.Enabled = True
            Fail()
        End Sub

    End Class

    Class PatrolRequest : Inherits Request

        'Mark a page as patrolled

        Public Page As Page

        Public Sub Start()
            LogProgress("Marking '" & Page.Name & "' as patrolled...")

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

            Dim Result As String

            If Page.Rcid Is Nothing Then
                Result = GetText("title=Special:Newpages&namespace=all&limit=500")

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

            Result = GetText("title=" & UrlEncode(Page.Name) & "&action=markpatrolled&rcid=" & UrlEncode(Page.Rcid))

            If Result.Contains("Marked as patrolled") Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            Page.Patrolled = True
            Log("Marked '" & Page.Name & "' as patrolled")
            Complete()
        End Sub

        Private Sub NotFound()
            Page.Patrolled = True
            Log("Did not mark '" & Page.Name & "' as patrolled; cannot find it in the new page log")
            Fail()
        End Sub

        Private Sub AlreadyPatrolled()
            Page.Patrolled = True
            Log("'" & Page.Name & "' has already been patrolled")
            Complete()
        End Sub

        Private Sub Failed()
            Log("Failed to mark '" & Page.Name & "' as patrolled")
            Fail()
        End Sub

    End Class

    Class MoveRequest : Inherits Request

        'Move a page

        Public Page As Page, Target, Reason As String

        Public Sub Start()
            LogProgress("Moving '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()

            Dim Result As String = GetText("title=Special:MovePage&target=" & UrlEncode(Page.Name))

            'If you see a page with permission errors stop the process
            If Result.Contains("<div class=""permissions-errors"">") Then
                Callback(AddressOf PermissionDenied)
                Exit Sub
            End If

            Dim EditTokenMatch As Match = Regex.Match(Result, _
                "<input name=""wpEditToken"" type=""hidden"" value=""(.*?)"" />", RegexOptions.Compiled)

            'If there is no success stop the process
            If Not EditTokenMatch.Success Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Dim EditToken As String = EditTokenMatch.Groups(1).Value
            Dim PostString As String = "wpOldTitle=" & UrlEncode(Page.Name) & "&wpNewTitle=" & UrlEncode(Target) & _
                "&wpReason=" & UrlEncode(Reason) & "&wpEditToken=" & UrlEncode(EditToken)

            'If the tickbox for moving page talk also is ticked append this.
            If MoveForm.MoveTalk.Checked Then
                PostString = PostString & "wpMovetalk=1"
            End If

            'Post the move request
            Result = PostData("title=Special:MovePage&target=" & UrlEncode(Page.Name) & "&action=submit", PostString)

            'If the move has suceeded call done, if not call fail
            If Result.Contains("<h1 class=""firstHeading"">Move succeeded</h1>") _
                Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            'When the page has been moved and done is called write to log
            Log("Moved '" & Page.Name & "' to '" & Target & "'")
            MainForm.PageB.Text = Target
            Complete()
        End Sub

        Private Sub Failed()
            'If the page move has failed write this to log
            Log("Did not move '" & Page.Name & "' to '" & Target & "'; the target might already exist")
            Fail()
        End Sub

        Private Sub PermissionDenied()
            'If the page move has failed due to permissions write this to log
            Log("Did not move '" & Page.Name & "' to '" & Target & "' – permission denied.")
            Fail()
        End Sub

    End Class

    Class DeleteRequest : Inherits Request

        'Delete a page

        Public Page As Page, Summary As String

        Public Sub Start()
            Log("Deleting '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Result As String = GetText("title=" & UrlEncode(Page.Name) & "&action=delete")

            Dim EditTokenMatch As Match = Regex.Match(Result, _
                "<input name=""wpEditToken"" type=""hidden"" value=""(.*?)"" />", RegexOptions.Compiled)

            If Not EditTokenMatch.Success Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            'Use default summary if none supplied
            If Summary = "" AndAlso Result.Contains("<input name=""wpReason""") Then
                Summary = Result.Substring(Result.IndexOf("<input name=""wpReason"""))
                Summary = Summary.Substring(Summary.IndexOf(" value=""") + 8)
                Summary = Summary.Substring(0, Summary.IndexOf(""""))
                Summary = HtmlDecode(Summary)
            End If

            Dim EditToken As String = EditTokenMatch.Groups(1).Value
            Dim PostString As String = "wpReason=" & UrlEncode(Summary) & "&wpEditToken=" & UrlEncode(EditToken)

            Result = PostData("title=" & UrlEncode(Page.Name) & "&action=delete", PostString)

            If Result.Contains("<h1 class=""firstHeading"">Action complete</h1>") _
                Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            Log("Deleted '" & Page.Name & "'")
            Page.DeletesCurrent = False
            Complete()
        End Sub

        Private Sub Failed()
            Log("Failed to delete '" & Page.Name & "'")
            If CurrentEdit.Page Is Page Then MainForm.PageDeleteB.Enabled = True
            Fail()
        End Sub

    End Class

    Class ProtectRequest : Inherits Request

        'Protect a page

        Public Page As Page, EditLevel, MoveLevel, Expiry, Summary As String, Cascade As Boolean

        Public Sub Start()
            LogProgress("Protecting '" & Page.Name & "'...")

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Result As String = GetText("title=" & UrlEncode(Page.Name) & "&action=protect")

            Dim EditTokenMatch As Match = Regex.Match(Result, _
                "<input type=""hidden"" name=""wpEditToken"" value=""(.*?)"" />", RegexOptions.Compiled)

            If Not EditTokenMatch.Success Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Dim EditToken As String = EditTokenMatch.Groups(1).Value

            Dim PostString As String = "mwProtect-reason=" & UrlEncode(Summary) _
                    & "&wpEditToken=" & UrlEncode(EditToken) & "&mwProtect-level-edit=" & UrlEncode(EditLevel) _
                    & "&mwProtect-level-move=" & UrlEncode(MoveLevel) & "&mwProtect-expiry=" & UrlEncode(Expiry)

            If Cascade Then PostString &= "&mwProtect-cascade=1"

            Result = PostData("title=" & UrlEncode(Page.Name) & "&action=protect", PostString)

            If Result IsNot Nothing Then Callback(AddressOf Done) Else Callback(AddressOf Failed)
        End Sub

        Private Sub Done()
            Log("Protected '" & Page.Name & "'")
            Page.ProtectionsCurrent = False
            Complete()
        End Sub

        Private Sub Failed()
            Log("Failed to protect '" & Page.Name & "'")
            Fail()
        End Sub

    End Class

    Class EmailRequest : Inherits Request

        'E-mail a user

        Public User As User, Subject As String = Config.EmailSubject, Message As String, CcMe, ShowForm As Boolean
        Private Token As String

        Public Sub GetForm()
            LogProgress("Getting e-mail form for '" & User.Name & "'...")

            Dim RequestThread As New Thread(AddressOf GetProcess)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub GetProcess()
            Dim Result As String = GetText("title=Special:EmailUser&target=" & UrlEncode(User.Name))

            If IsWikiPage(Result) AndAlso Not Result.Contains("<form id=""emailuser""") Then
                Callback(AddressOf NoEmail)
                Exit Sub
            End If

            Dim EditTokenMatch As Match = Regex.Match(Result, _
                "<input type='hidden' name='wpEditToken' value=""(.*?)"" />", RegexOptions.Compiled)

            If Not EditTokenMatch.Success Then
                Callback(AddressOf Failed)
                Exit Sub
            End If

            Token = EditTokenMatch.Groups(1).Value
            Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            If ShowForm Then
                DelogProgress()

                Dim NewEmailForm As New EmailForm
                NewEmailForm.User = User
                NewEmailForm.Subject.Text = Subject
                NewEmailForm.Message.Text = Message
                NewEmailForm.Request = Me
                NewEmailForm.Show()
            Else
                PostForm()
            End If
        End Sub

        Private Sub Failed()
            Log("Failed to retrieve e-mail form for '" & User.Name & "'")
            Fail()
        End Sub

        Private Sub NoEmail()
            If MessageBox.Show("The user '" & User.Name & "' does not have e-mail enabled." & LF & _
                "Post a discussion page message instead?", "Huggle", MessageBoxButtons.YesNo, _
                MessageBoxIcon.Exclamation) = DialogResult.Yes Then

                Dim NewMessageForm As New MessageForm
                NewMessageForm.User = User
                NewMessageForm.Show()
            End If

            Fail()
        End Sub

        Public Sub PostForm()
            LogProgress("Submitting e-mail form for '" & User.Name & "'...")

            Dim RequestThread As New Thread(AddressOf PostProcess)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub PostProcess()
            Dim PostString As String = "wpSubject=" & UrlEncode(Subject) & _
                "&wpText=" & UrlEncode(Message) & "&wpEditToken=" & UrlEncode(Token)

            If CcMe Then PostString &= "&wpCCMe=1"

            Dim Result As String = PostData("title=Special:EmailUser&target=" & UrlEncode(User.Name) & _
                "&action=submit", PostString)

            If IsWikiPage(Result) Then Callback(AddressOf PostDone) Else Callback(AddressOf PostFailed)
        End Sub

        Private Sub PostDone()
            Log("Sent e-mail to '" & User.Name & "'")
            Complete()
        End Sub

        Private Sub PostFailed()
            Log("Failed to submit e-mail form for '" & User.Name & "'")
            Fail()
        End Sub

    End Class

    Class UpdateRequest : Inherits Request

        'Download latest version of the application

        Public FileName As String

        Public Sub Start(Optional ByVal Done As RequestCallback = Nothing)
            _Done = Done

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim Client As New WebClient

            Client.Headers.Add(HttpRequestHeader.UserAgent, Config.UserAgent)

            Try
                Client.DownloadFile(Config.DownloadLocation.Replace("$1", VersionString(Config.LatestVersion)), FileName)
            Catch ex As WebException
                If State = States.Cancelled Then Thread.CurrentThread.Abort()
                Callback(AddressOf Failed)
            End Try

            If State = States.Cancelled Then Thread.CurrentThread.Abort()
            Callback(AddressOf Done)
        End Sub

        Private Sub Done()
            Complete()
        End Sub

        Private Sub Failed()
            Fail()
        End Sub

    End Class

End Namespace