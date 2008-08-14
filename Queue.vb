Imports System.IO
Imports System.Text.RegularExpressions

Class Queue

    'Represents a queue of edits

    Private _Initialised As Boolean
    Private _Pages As New List(Of String)
    Private _Items As New List(Of Edit)
    Private _Type As Types

    Private _Spaces As New List(Of Space)
    Private _FilterNewPage As CheckState
    Private _PageRegex, UserRegex As Regex

    Public Enum Types As Integer
        : FixedList : LiveList : Live
    End Enum

    Public Shared Operator =(ByVal a As Queue, ByVal b As Queue) As Boolean
        Return (a Is b)
    End Operator

    Public Shared Operator <>(ByVal a As Queue, ByVal b As Queue) As Boolean
        Return (a IsNot b)
    End Operator

    Public Property Initialised() As Boolean
        Get
            Return _Initialised
        End Get
        Set(ByVal value As Boolean)
            _Initialised = value
        End Set
    End Property

    Public Property Pages() As Boolean
        Get
            Return _Initialised
        End Get
        Set(ByVal value As Boolean)
            _Initialised = value
        End Set
    End Property

    Public Sub Initialise()
        'Clear a live queue or populate a fixed one
        Items.Clear()

        If Type = Types.FixedList Then
            For Each Item As String In Pages
                Dim Page As Page = GetPage(Item)

                If Page.LastEdit IsNot Nothing Then
                    CurrentQueue.Items.Add(Page.LastEdit)
                Else
                    Dim NewEdit As New Edit
                    NewEdit.Page = Page
                    NewEdit.Id = "cur"
                    NewEdit.Oldid = "prev"
                    CurrentQueue.Items.Add(NewEdit)
                End If
            Next Item
        End If

        Initialised = True
    End Sub

    Public Function MatchesFilter(ByVal PageName As String) As Boolean
        'Determines whether a page name matches this queue's filter
        Dim Page As Page = GetPage(PageName)

        If PageRegex IsNot Nothing AndAlso Not PageRegex.IsMatch(Page.Name) Then Return False

        Return True
    End Function

    Public Function MatchesFilter(ByVal Edit As Edit) As Boolean
        'Determines whether an edit matches this queue's filter
        If Type = Types.FixedList Then Return False
        If Type = Types.LiveList AndAlso Not Pages.Contains(Edit.Page.Name) Then Return False
        If Spaces.Count > 0 AndAlso Not Spaces.Contains(Edit.Page.Space) Then Return False
        If PageRegex IsNot Nothing AndAlso Not PageRegex.IsMatch(Edit.Page.Name) Then Return False
        If UserRegex IsNot Nothing AndAlso Not UserRegex.IsMatch(Edit.User.Name) Then Return False

        Return CSMatch(FilterNewPage, Edit.NewPage)
    End Function

    Private Function CSMatch(ByVal Filter As CheckState, ByVal Value As Boolean) As Boolean
        'Helper function for above
        Select Case Filter
            Case CheckState.Indeterminate : Return True
            Case CheckState.Checked : Return Value
            Case CheckState.Unchecked : Return Not Value
        End Select
    End Function

    Public Shared Sub LoadQueues()
        AllQueues.Clear()

        'Load queues from application data subfolder
        If Directory.Exists(QueuesLocation) Then
            For Each Path As String In Directory.GetFiles(QueuesLocation)
                Dim Items As New List(Of String)(File.ReadAllLines(Path))
                Dim Queue As New Queue, Name As String = Nothing

                For Each Item As String In Items
                    If Item.Contains(":") Then
                        Dim OptionName As String = Item.Substring(0, Item.IndexOf(":")), _
                            OptionValue As String = Item.Substring(Item.IndexOf(":") + 1)

                        Try
                            Select Case OptionName
                                Case "name" : Name = OptionValue
                                Case "new-pages" : Queue.FilterNewPage = CType(OptionValue, CheckState)
                                Case "page-regex" : Queue.PageRegex = New Regex(OptionValue)
                                Case "pages" : Queue.Pages.AddRange(OptionValue.Split("|"c))
                                Case "spaces" : Queue.Spaces.AddRange(SetQueueSpaces(OptionValue))
                                Case "type" : Queue.Type = SetQueueType(OptionValue)
                                Case "user-regex" : Queue.UserRegex = New Regex(OptionValue)
                            End Select

                        Catch ex As Exception
                            'Ignore malformed config entries
                        End Try
                    End If
                Next Item

                If Name IsNot Nothing Then AllQueues.Add(Name, Queue)
            Next Path
        End If
    End Sub

    Private Shared Function SetQueueSpaces(ByVal Value As String) As List(Of Space)
        'Helper function for above
        Dim Spaces As New List(Of Space)

        For Each Item As String In Value.Split(","c)
            If Not Spaces.Contains(Space.GetSpace(CInt(Item))) Then Spaces.Add(Space.GetSpace(CInt(Item)))
        Next Item

        Return Spaces
    End Function

    Private Shared Function SetQueueType(ByVal Value As String) As Types
        'Helper function for above
        Select Case Value
            Case "FixedList" : Return Types.FixedList
            Case "LiveList" : Return Types.LiveList
            Case Else : Return Types.Live
        End Select
    End Function

    Public Shared Sub SaveQueues()
        'Create subfolder if it does not exist
        If Not Directory.Exists(QueuesLocation) AndAlso Not Directory.CreateDirectory(QueuesLocation).Exists Then
            MsgBox("Unable to save edit queues; could not create sub-folder.", MsgBoxStyle.Critical, "Huggle")
            Exit Sub
        End If

        'Write queues to application data subfolder
        For Each Queue As KeyValuePair(Of String, Queue) In AllQueues
            Dim Items As New List(Of String)

            Items.Add("name:" & Queue.Key)
            Items.Add("type:" & Queue.Value.Type.ToString)
            Items.Add("new-pages:" & CStr(CInt(Queue.Value.FilterNewPage)))

            If Queue.Value.Spaces.Count > 0 Then
                Dim Spaces As New List(Of String)

                For Each Space As Space In Queue.Value.Spaces
                    Spaces.Add(CStr(Space.Number))
                Next Space

                Items.Add("spaces:" & String.Join(",", Spaces.ToArray))
            End If

            If Queue.Value.PageRegex IsNot Nothing Then Items.Add("page-regex:" & Queue.Value.PageRegex.ToString)
            If Queue.Value.UserRegex IsNot Nothing Then Items.Add("user-regex:" & Queue.Value.UserRegex.ToString)
            If Queue.Value.Pages.Count > 0 Then Items.Add("pages:" & String.Join("|", Queue.Value.Pages.ToArray))

            File.WriteAllLines(QueuesLocation() & "\" & Queue.Key & ".txt", Items.ToArray)
        Next Queue
    End Sub

    Private Shared ReadOnly Property QueuesLocation() As String
        Get
            Return LocalConfigPath() & "\Queues\" & Config.Project
        End Get
    End Property

End Class