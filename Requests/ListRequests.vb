Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web.HttpUtility

Namespace Requests

    MustInherit Class ListRequest : Inherits Request

        'Abstract class for getting a list of pages through the API

        Protected QueryParams, TypeName, TypePrefix, Page As String
        Protected Shadows _Done As ListRequestCallback
        Protected Progress As ListProgressCallback, Items As New List(Of String)

        Public Delegate Sub ListRequestCallback(ByVal Result As List(Of String))
        Public Delegate Sub ListProgressCallback(ByVal State As String, ByVal PartialResult As List(Of String))

        Public Limit As Integer = ApiLimit(), From As String = "", List As List(Of String), TitleRegex As Regex
        Public Spaces As New List(Of Space)

        Public Sub New(ByVal _TypeName As String, ByVal _TypePrefix As String, ByVal _QueryParams As String)
            TypeName = _TypeName
            TypePrefix = _TypePrefix
            QueryParams = _QueryParams
        End Sub

        Public Overridable Sub Start(ByVal Done As ListRequestCallback, _
            Optional ByVal _Progress As ListProgressCallback = Nothing)

            _Done = Done
            If _Progress IsNot Nothing Then Progress = _Progress

            Dim RequestThread As New Thread(AddressOf Process)
            RequestThread.IsBackground = True
            RequestThread.Start()
        End Sub

        Private Sub Process()
            Dim ContinueFrom As String = Nothing, Remaining As Integer = Limit

            Do
                Dim QueryString As String = "action=query&format=xml&list=" & TypeName & "&" & TypePrefix & _
                    "limit=" & Math.Min(Remaining, ApiLimit()) & "&" & QueryParams
                If ContinueFrom IsNot Nothing Then QueryString &= "&" & TypePrefix & "continue=" & ContinueFrom

                Dim Result As String = GetApi(QueryString)

                If Result Is Nothing Then
                    Callback(AddressOf Failed)
                    Exit Sub

                ElseIf Result.Contains("<" & TypeName & " />") Then
                    Callback(AddressOf RequestDone)
                    Exit Sub
                End If

                If Result.Contains("<query-continue>") Then
                    ContinueFrom = Result.Substring(Result.IndexOf(TypePrefix & "continue=""") + 12)
                    ContinueFrom = ContinueFrom.Substring(0, ContinueFrom.IndexOf(""""))
                    ContinueFrom = HtmlDecode(ContinueFrom)
                Else
                    ContinueFrom = Nothing
                End If

                Result = Result.Substring(Result.IndexOf("<" & TypeName & ">") + 17)
                Result = Result.Substring(0, Result.IndexOf("</" & TypeName & ">"))

                Dim ItemsAdded As Boolean = False

                For Each Item As String In Result.Split("<"c)
                    If Item.Contains("title=""") Then
                        Item = Item.Substring(Item.IndexOf("title=""") + 7)
                        Item = Item.Substring(0, Item.IndexOf(""""))
                        Item = HtmlDecode(Item)

                        If Not Items.Contains(Item) AndAlso MatchesFilter(Item) Then
                            Items.Add(Item)
                            Remaining -= 1
                            If Remaining <= 0 Then Exit Do
                            ItemsAdded = True
                        End If
                    End If
                Next Item

                If ContinueFrom IsNot Nothing AndAlso ItemsAdded Then Callback(AddressOf Progressed, CObj(Items))

            Loop Until ContinueFrom Is Nothing

            Callback(AddressOf RequestDone)
        End Sub

        Protected Overridable Function MatchesFilter(ByVal Title As String) As Boolean
            If Title < From Then Return False
            If Not Spaces.Contains(GetPage(Title).Space) Then Return False
            If TitleRegex IsNot Nothing AndAlso Not TitleRegex.IsMatch(Title) Then Return False
            Return True
        End Function

        Private Sub RequestDone()
            Complete()
            _Done(Items)
        End Sub

        Private Sub Progressed(ByVal ListObject As Object)
            If Progress IsNot Nothing Then Progress("Running query...", CType(ListObject, List(Of String)))
        End Sub

        Private Sub Failed()
            Fail()
            _Done(Nothing)
        End Sub

    End Class

    Class CategoryRequest : Inherits ListRequest

        'Get the contents of a category

        Sub New(ByVal Category As String)
            MyBase.New("categorymembers", "cm", "&cmprop=title&cmtitle=" & UrlEncode("Category:" & Category))
        End Sub

    End Class

    Class BacklinksRequest : Inherits ListRequest

        'Get pages that link to another page

        Sub New(ByVal Page As String)
            MyBase.New("backlinks", "bl", "blfilterredir=nonredirects&bltitle=" & UrlEncode(Page))
        End Sub

    End Class

    Class TransclusionsRequest : Inherits ListRequest

        'Get pages that transclude another page

        Sub New(ByVal Page As String)
            MyBase.New("embeddedin", "ei", "eititle=" & UrlEncode(Page))
        End Sub

    End Class

    Class ImageUsageRequest : Inherits ListRequest

        'Get pages that include an image

        Sub New(ByVal ImageName As String)
            MyBase.New("imageusage", "iu", "iutitle=" & UrlEncode("Image:" & ImageName))
        End Sub

    End Class

    Class SearchRequest : Inherits ListRequest

        'Get search results

        Sub New(ByVal Page As String)
            MyBase.New("search", "sr", "srsearch=" & UrlEncode(Page) & "&srwhat=text")
        End Sub

    End Class

    Class ContribsListRequest : Inherits ListRequest

        'Get pages edited by a user

        Sub New(ByVal User As String)
            MyBase.New("usercontribs", "uc", "ucuser=" & UrlEncode(User))
        End Sub

    End Class

    Class ExternalLinkUsageRequest : Inherits ListRequest

        'Get pages that use an external link

        Sub New(ByVal Link As String)
            MyBase.New("exturlusage", "eu", "euquery=" & UrlEncode(Link))
        End Sub

    End Class

    Class LinksRequest : Inherits ListRequest

        'Get links on a page

        Sub New(ByVal Page As String)
            MyBase.New("links", "pl", "prop=links&titles=" & UrlEncode(Page))
        End Sub

    End Class

    Class ImagesRequest : Inherits ListRequest

        'Get images on a page

        Sub New(ByVal Page As String)
            MyBase.New("images", "im", "prop=images&titles=" & UrlEncode(Page))
        End Sub

    End Class

    Class TemplatesRequest : Inherits ListRequest

        'Get templates on a page

        Sub New(ByVal Page As String)
            MyBase.New("templates", "tl", "prop=templates&titles=" & UrlEncode(Page))
        End Sub

    End Class

    Class RecursiveCategoryRequest : Inherits ListRequest

        'Recursively get the contents of a category

        Private AllItems As New List(Of String)
        Private Category As String, CategoriesDone As New List(Of String), CategoriesRemaining As New List(Of String)
        Private Shadows _Done As ListRequestCallback, Progress As ListProgressCallback
        Public Shadows From As String = "", Queue As Queue, Spaces As List(Of Space)

        Public Sub New(ByVal _Category As String)
            MyBase.New("categorymembers", "cm", "cmprop=title&cmtitle=" & UrlEncode("Category:" & _Category))
            Category = _Category
        End Sub

        Public Overrides Sub Start(ByVal Done As ListRequestCallback, _
            Optional ByVal _Progress As ListProgressCallback = Nothing)

            _Done = Done

            'Use a copy of the queue in the base class, but change the filters so we always get categories back
            From = MyBase.From
            MyBase.From = ""
            Spaces = New List(Of Space)(MyBase.Spaces)
            If Not MyBase.Spaces.Contains(Space.Category) Then MyBase.Spaces.Add(Space.Category)

            If _Progress IsNot Nothing Then
                Progress = _Progress
                Progress("Getting Category:" & Category, Nothing)
            End If

            MyBase.Start(AddressOf CategoryDone)
        End Sub

        Private Sub CategoryDone(ByVal Items As List(Of String))
            If Items Is Nothing Then
                AllDone()
            Else
                For Each Item As String In Items
                    If Item.StartsWith("Category:") AndAlso Not CategoriesDone.Contains(Item) _
                        AndAlso Not CategoriesRemaining.Contains(Item) Then CategoriesRemaining.Add(Item)

                    If Not AllItems.Contains(Item) AndAlso MatchesFilter(Item) Then
                        AllItems.Add(Item)

                        If AllItems.Count >= Limit Then
                            AllDone()
                            Exit Sub
                        End If
                    End If
                Next Item

                If CategoriesRemaining.Count = 0 Then
                    AllDone()
                Else
                    If Progress IsNot Nothing Then Progress("Getting " & CategoriesRemaining(0) & "...", AllItems)
                    MyBase.QueryParams = "cmprop=title&cmtitle=" & UrlEncode(CategoriesRemaining(0))
                    CategoriesDone.Add(CategoriesRemaining(0))
                    CategoriesRemaining.RemoveAt(0)
                    MyBase.Start(AddressOf CategoryDone)
                End If
            End If
        End Sub

        Protected Overrides Function MatchesFilter(ByVal Title As String) As Boolean
            If Title < From Then Return False
            If Not Spaces.Contains(GetPage(Title).Space) Then Return False
            If TitleRegex IsNot Nothing AndAlso Not TitleRegex.IsMatch(Title) Then Return False
            Return True
        End Function

        Private Sub AllDone()
            _Done(AllItems)
        End Sub

    End Class

End Namespace