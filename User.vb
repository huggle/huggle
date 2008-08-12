<DebuggerDisplay("{Name}")> _
Class User

    'Represents a user account or anonymous user

    Private _Level As UserL
    Private _SharedIP As Boolean
    Private _EditCount As Integer = -1
    Private _SessionEditCount As Integer

    Public AutoRevert As Boolean
    Public Name As String
    Public FirstEdit As Edit
    Public LastEdit As Edit
    Public Anonymous As Boolean
    Public WarnTime As Date
    Public Warnings As List(Of Warning)
    Public WarningsCurrent As Boolean
    Public Blocks As List(Of Block)
    Public BlocksCurrent As Boolean
    Public Bot As Boolean
    Public ContribsOffset As String

    Public Property Level() As UserL
        <DebuggerStepThrough()> Get
            Return _Level
        End Get
        <DebuggerStepThrough()> Set(ByVal value As UserL)
            _Level = value

            Dim Redraw, Sort As Boolean

            If value > UserL.Ignore Then
                Dim ThisEdit As Edit = LastEdit

                While ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
                    If ThisEdit Is ThisEdit.Page.LastEdit AndAlso Not ThisEdit.Added _
                        AndAlso Not FilteredEdits.Items.Contains(ThisEdit) Then

                        If ThisEdit.User.Level <> UserL.Ignore _
                            AndAlso (Config.ShowNewPages OrElse Not ThisEdit.NewPage) _
                            AndAlso ThisEdit.Page.Level <> Page.Levels.Ignore _
                            AndAlso Not OwnUserspace(ThisEdit) _
                            AndAlso ThisEdit.Type >= Edit.Types.None _
                            AndAlso Not Math.Abs(ThisEdit.Size) > 100000 Then

                            Dim LCSpace As String = ThisEdit.Page.Namespace.ToLower
                            If LCSpace = "" Then LCSpace = "article"

                            If Config.NamespacesChecked.Contains(LCSpace) Then
                                FilteredEdits.Items.Add(ThisEdit)
                                ThisEdit.Added = True
                                MainForm.DiffNextB.Enabled = True
                                If FilteredEdits.Items.Count > 5000 Then FilteredEdits.Items.RemoveAt(5000)
                                Redraw = True
                                Sort = True
                            End If
                        End If
                    End If

                    ThisEdit = ThisEdit.PrevByUser
                End While
            End If

            For Each Item As Form In Application.OpenForms
                Dim uif As UserInfoForm = TryCast(Item, UserInfoForm)

                If uif IsNot Nothing AndAlso uif.ThisUser Is Me _
                    Then If _Level = UserL.Ignore Then uif.Whitelisted.Text = "Yes" _
                    Else uif.Whitelisted.Text = "No"
            Next Item

            'Redraw contribs and queue if necessary
            If MainForm IsNot Nothing Then
                If CurrentEdit IsNot Nothing AndAlso CurrentEdit.User Is Me Then MainForm.DrawContribs()

                If Config.ShowQueue Then
                    For i As Integer = 0 To Math.Min(FilteredEdits.Items.Count - 1, (MainForm.QueuePanel.Height \ 20) - 2)
                        If FilteredEdits.Items(i).User Is Me Then
                            Redraw = True
                            Exit For
                        End If
                    Next i
                End If

                If Redraw Then MainForm.DrawQueue()
            End If

            If Sort Then FilteredEdits.Items.Sort(AddressOf Edit.Compare)
        End Set
    End Property

    Public Property SharedIP() As Boolean
        Get
            Return _SharedIP
        End Get
        Set(ByVal value As Boolean)
            _SharedIP = value

            For Each Item As Form In Application.OpenForms
                Dim uif As UserInfoForm = TryCast(Item, UserInfoForm)

                If uif IsNot Nothing AndAlso uif.ThisUser Is Me _
                    Then If _SharedIP Then uif.SharedIP.Text = "Yes" _
                    Else uif.SharedIP.Text = "No"
            Next Item
        End Set
    End Property

    Public Property EditCount() As Integer
        Get
            Return _EditCount
        End Get
        Set(ByVal value As Integer)
            _EditCount = value

            For Each Item As Form In Application.OpenForms
                Dim uif As UserInfoForm = TryCast(Item, UserInfoForm)

                If uif IsNot Nothing AndAlso uif.ThisUser Is Me _
                    Then uif.EditCount.Text = CStr(_EditCount)
            Next Item
        End Set
    End Property

    Public Property SessionEditCount() As Integer
        Get
            Return _SessionEditCount
        End Get
        Set(ByVal value As Integer)
            _SessionEditCount = value

            For Each Item As Form In Application.OpenForms
                Dim uif As UserInfoForm = TryCast(Item, UserInfoForm)

                If uif IsNot Nothing AndAlso uif.ThisUser Is Me _
                    Then uif.SessionEditCount.Text = CStr(_SessionEditCount)
            Next Item
        End Set
    End Property

End Class