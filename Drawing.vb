Module Drawing

    Public Function History(ByVal Page As Page) As Image
        If MainForm.History.Width < 0 Then Return Nothing
        Dim Result As New Bitmap(MainForm.History.Width, 20)
        Dim Gfx As Graphics = Graphics.FromImage(Result)

        Gfx.Clear(Color.FromKnownColor(KnownColor.Control))
        Gfx.DrawImage(My.Resources.gradient, 2, 2, Result.Width - 4, Result.Height - 4)
        Gfx.DrawRectangle(Pens.DarkGray, 1, 1, Result.Width - 3, Result.Height - 3)

        If Page IsNot Nothing Then
            Dim CurrentPosition As Integer = -1, OldPosition As Integer = -1
            Dim Edit As Edit = Page.LastEdit
            Dim X As Integer = Result.Width - 18 + (HistoryOffset * 17)

            While Edit IsNot Nothing AndAlso Edit IsNot NullEdit
                If X < 0 Then Exit While

                If X < Result.Width Then
                    Gfx.DrawImage(Edit.Icon, X, 2)

                    Select Case Edit.WarningLevel
                        Case UserLevel.Warn1 : Gfx.DrawString("!1", MainForm.Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn2 : Gfx.DrawString("!2", MainForm.Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn3 : Gfx.DrawString("!3", MainForm.Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn4im, UserLevel.WarnFinal : Gfx.DrawString("!4", MainForm.Font, Brushes.Black, X + 2, 3)
                    End Select

                    If Edit.Id = CurrentEdit.Id Then
                        CurrentPosition = X - 1
                        If Not CurrentEdit.Multiple Then OldPosition = X - 18
                    End If

                    If Edit.Page IsNot Nothing AndAlso Edit Is Edit.Page.LastEdit _
                        Then Gfx.DrawRectangle(Pens.DarkBlue, X, 2, 15, 15)
                    If CurrentEdit.Multiple AndAlso Edit.Id = CurrentEdit.Oldid Then OldPosition = X - 1
                    Gfx.DrawLine(Pens.DarkGray, X + 16, 2, X + 16, 17)
                End If

                X -= 17
                Edit = Edit.Prev
            End While

            If X < 0 AndAlso Edit IsNot Nothing AndAlso (Edit Is CurrentEdit _
                OrElse Edit.Id = CurrentEdit.Oldid) AndAlso Not (Edit Is Page.LastEdit _
                OrElse Edit Is Page.LastEdit.Prev) Then

                HistoryOffset += 1
                Return History(Page)
            End If

            Gfx.DrawLine(Pens.DarkGray, X + 16, 2, X + 16, 17)

            'Draw selection box
            If CurrentPosition > -1 AndAlso OldPosition > -1 Then
                If CurrentEdit.Multiple Then
                    Gfx.DrawRectangle(New Pen(Color.Red, 2), CurrentPosition, 1, 18, 18)
                    If OldPosition > -1 Then Gfx.DrawRectangle(New Pen(Color.Red, 2), OldPosition, 1, 18, 18)
                Else
                    Gfx.DrawRectangle(New Pen(Color.Red, 2), OldPosition, 1, 35, 18)
                End If
            End If

            'Draw end-of-history box
            If Edit Is NullEdit Then Gfx.FillRectangle(Brushes.Black, X, 2, 16, 16)
        End If

        Return Result
    End Function

    Public Function Contribs(ByVal User As User) As Image
        If MainForm.Contribs.Width < 0 Then Return Nothing
        Dim Result As New Bitmap(MainForm.Contribs.Width, 20)
        Dim Gfx As Graphics = Graphics.FromImage(Result)

        Gfx.Clear(Color.FromKnownColor(KnownColor.Control))
        Gfx.DrawImage(My.Resources.gradient, 2, 2, Result.Width - 4, Result.Height - 4)
        Gfx.DrawRectangle(Pens.DarkGray, 1, 1, Result.Width - 3, Result.Height - 3)

        If User IsNot Nothing Then
            Dim CurrentPosition As Integer = 0
            Dim ThisEdit As Edit = User.LastEdit
            Dim X As Integer = Result.Width - 18 + (ContribsOffset * 17)

            While ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
                If X < 0 Then Exit While

                'Draw icon
                If X < Result.Width Then
                    Gfx.DrawImage(ThisEdit.Icon, X, 2)

                    Select Case ThisEdit.WarningLevel
                        Case UserLevel.Warn1 : Gfx.DrawString("!1", MainForm.Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn2 : Gfx.DrawString("!2", MainForm.Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn3 : Gfx.DrawString("!3", MainForm.Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn4im, UserLevel.WarnFinal : Gfx.DrawString("!4", MainForm.Font, Brushes.Black, X + 2, 3)
                    End Select

                    If ThisEdit Is ThisEdit.Page.LastEdit Then Gfx.DrawRectangle(Pens.DarkBlue, X, 2, 15, 15)
                    If ThisEdit Is CurrentEdit Then CurrentPosition = X
                    Gfx.DrawLine(Pens.DarkGray, X + 16, 2, X + 16, 17)
                End If

                X -= 17
                ThisEdit = ThisEdit.PrevByUser
            End While

            Gfx.DrawLine(Pens.DarkGray, X + 16, 2, X + 16, 17)

            'Draw selection box
            If CurrentPosition > 0 Then Gfx.DrawRectangle(New Pen(Color.Red, 2), CurrentPosition - 1, 1, 18, 18)

            'Draw end-of-contribs box
            If ThisEdit Is NullEdit Then Gfx.FillRectangle(Brushes.Black, X, 2, 16, 16)
        End If

        Return Result
    End Function

End Module
