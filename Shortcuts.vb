Module Shortcuts

    Public ShortcutKeys As New Dictionary(Of String, Shortcut)

    Class Shortcut

        Public Control, Alt, Shift As Boolean, Key As Keys

        Public Sub New(ByVal Key As Keys, Optional ByVal Control As Boolean = False, _
            Optional ByVal Alt As Boolean = False, Optional ByVal Shift As Boolean = False)

            Me.Control = Control
            Me.Alt = Alt
            Me.Shift = Shift
            Me.Key = Key
        End Sub

        Public Shared Operator =(ByVal a As Shortcut, ByVal b As Shortcut) As Boolean
            Return (a.Alt = b.Alt AndAlso a.Control = b.Control AndAlso a.Shift = b.Shift AndAlso a.Key = b.Key)
        End Operator

        Public Shared Operator <>(ByVal a As Shortcut, ByVal b As Shortcut) As Boolean
            Return Not (a = b)
        End Operator

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim shortc As Shortcut = TryCast(obj, Shortcut)

            If shortc Is Nothing Then
                Return False
            Else
                Return (Me = shortc) '(shortc.Alt = Me.Alt AndAlso shortc.Control = Me.Control AndAlso shortc.Key = Me.Key AndAlso shortc.Shift = Me.Shift)
            End If

        End Function

        Public Overrides Function ToString() As String
            Dim Name As String = Key.ToString

            Dim NameReplacements As String() = { _
                "OemQuestion", "?", _
                "OemOpenBrackets", "[", _
                "OemCloseBrackets", "]", _
                "Oem6", "]", _
                "Oemplus", "+", _
                "OemMinus", "–", _
                "Oemcomma", ",", _
                "OemPeriod", "." _
                }

            For i As Integer = 0 To NameReplacements.Length - 2 Step 2
                If Name = NameReplacements(i) Then
                    Name = NameReplacements(i + 1)
                    Exit For
                End If
            Next i

            If Shift Then Name = "Shift + " & Name
            If Alt Then Name = "Alt + " & Name
            If Control Then Name = "Ctrl + " & Name

            Return Name
        End Function

    End Class

    Public Sub InitialiseShortcuts()
        ShortcutKeys.Clear()

        ShortcutKeys("About") = New Shortcut(Keys.None)
        ShortcutKeys("Block user") = New Shortcut(Keys.B, True)
        ShortcutKeys("Browse back") = New Shortcut(Keys.OemOpenBrackets)
        ShortcutKeys("Browse forward") = New Shortcut(Keys.OemCloseBrackets)
        ShortcutKeys("Cancel") = New Shortcut(Keys.Escape)
        ShortcutKeys("Clear queue") = New Shortcut(Keys.Space, True)
        ShortcutKeys("Close tab") = New Shortcut(Keys.OemMinus)
        ShortcutKeys("Close other tabs") = New Shortcut(Keys.None)
        ShortcutKeys("Current revision") = New Shortcut(Keys.C)
        ShortcutKeys("Delete page") = New Shortcut(Keys.D, True)
        ShortcutKeys("Diff to current revision") = New Shortcut(Keys.D)
        ShortcutKeys("Edit page") = New Shortcut(Keys.E)
        ShortcutKeys("Help") = New Shortcut(Keys.F1)
        ShortcutKeys("Ignore user") = New Shortcut(Keys.I)
        ShortcutKeys("Latest contribution") = New Shortcut(Keys.C, True)
        ShortcutKeys("Mark page as patrolled") = New Shortcut(Keys.P, True)
        ShortcutKeys("Message user") = New Shortcut(Keys.N)
        ShortcutKeys("Move page") = New Shortcut(Keys.None)
        ShortcutKeys("New tab") = New Shortcut(Keys.Oemplus)
        ShortcutKeys("Next contribution") = New Shortcut(Keys.X, True)
        ShortcutKeys("Next revision") = New Shortcut(Keys.X)
        ShortcutKeys("Next tab") = New Shortcut(Keys.Tab)
        ShortcutKeys("Nominate for deletion") = New Shortcut(Keys.S, True)
        ShortcutKeys("Open page in external browser") = New Shortcut(Keys.O)
        ShortcutKeys("Post template message") = New Shortcut(Keys.T)
        ShortcutKeys("Previous contribution") = New Shortcut(Keys.Z, True)
        ShortcutKeys("Previous revision") = New Shortcut(Keys.Z)
        ShortcutKeys("Previous tab") = New Shortcut(Keys.Tab, , , True)
        ShortcutKeys("Proposed deletion") = New Shortcut(Keys.P)
        ShortcutKeys("Protect page") = New Shortcut(Keys.None)
        ShortcutKeys("Report user") = New Shortcut(Keys.B)
        ShortcutKeys("Request deletion") = New Shortcut(Keys.S)
        ShortcutKeys("Request protection") = New Shortcut(Keys.None)
        ShortcutKeys("Retrieve recent page history") = New Shortcut(Keys.H)
        ShortcutKeys("Retrieve full page history") = New Shortcut(Keys.H, True)
        ShortcutKeys("Retrieve user contributions") = New Shortcut(Keys.U)
        ShortcutKeys("Revert") = New Shortcut(Keys.R)
        ShortcutKeys("Revert and warn") = New Shortcut(Keys.Q)
        ShortcutKeys("Revert with custom summary") = New Shortcut(Keys.Y)
        ShortcutKeys("Show history page") = New Shortcut(Keys.None)
        ShortcutKeys("Show new messages") = New Shortcut(Keys.M)
        ShortcutKeys("Show next diff") = New Shortcut(Keys.Space)
        ShortcutKeys("Speedy deletion") = New Shortcut(Keys.None)
        ShortcutKeys("Tag page") = New Shortcut(Keys.G)
        ShortcutKeys("Toggle 'show new edits'") = New Shortcut(Keys.K)
        ShortcutKeys("Trim queue") = New Shortcut(Keys.None)
        ShortcutKeys("Unignore user") = New Shortcut(Keys.I, True)
        ShortcutKeys("User information") = New Shortcut(Keys.OemQuestion)
        ShortcutKeys("View current revision") = New Shortcut(Keys.V, True)
        ShortcutKeys("View this revision") = New Shortcut(Keys.V)
        ShortcutKeys("View user talk page") = New Shortcut(Keys.A)
        ShortcutKeys("Warn") = New Shortcut(Keys.W)
        ShortcutKeys("Watch page") = New Shortcut(Keys.L)
    End Sub

End Module
