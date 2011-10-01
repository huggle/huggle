Class ContribsStrip

    Inherits ToolStripItem

    Public User As User, Offset As Integer, SelectedEdit As Edit

    Public Sub Draw(ByVal s As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
        Dim Gfx As Graphics = e.Graphics

        Gfx.Clear(Color.FromKnownColor(KnownColor.Control))
        Gfx.DrawImage(My.Resources.gradient, 2, 2, Width - 4, Height - 4)
        Gfx.DrawRectangle(Pens.DarkGray, 1, 1, Width - 3, Height - 3)

        If User IsNot Nothing Then
            Dim CurrentPosition As Integer = 0
            Dim Edit As Edit = User.LastEdit
            Dim X As Integer = Width - 18 + (Offset * 17)

            While Edit IsNot Nothing AndAlso Edit IsNot NullEdit
                If X < 0 Then Exit While

                'Draw icon
                If X < Width Then
                    Try
                        Gfx.DrawImage(Edit.Icon, X, 2)
                    Catch ex As InvalidOperationException
                    End Try

                    Select Case Edit.WarningLevel
                        Case UserLevel.Warn1 : Gfx.DrawString("!1", Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn2 : Gfx.DrawString("!2", Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn3 : Gfx.DrawString("!3", Font, Brushes.Black, X + 2, 3)
                        Case UserLevel.Warn4im, UserLevel.WarnFinal : Gfx.DrawString("!4", Font, Brushes.Black, X + 2, 3)
                    End Select

                    If Edit.Assisted AndAlso Edit.Type = EditType.None AndAlso Edit.WarningLevel = UserLevel.None _
                        AndAlso Not Edit.Bot Then Gfx.DrawString("*", New Font(FontFamily.GenericSansSerif, _
                        14, FontStyle.Regular), Brushes.Black, X + 2, 3)

                    If Edit Is Edit.Page.LastEdit Then Gfx.DrawRectangle(Pens.DarkBlue, X, 2, 15, 15)
                    If Edit Is SelectedEdit Then CurrentPosition = X
                    Gfx.DrawLine(Pens.DarkGray, X + 16, 2, X + 16, 17)
                End If

                X -= 17
                Edit = Edit.PrevByUser
            End While

            Gfx.DrawLine(Pens.DarkGray, X + 16, 2, X + 16, 17)

            'Draw selection box
            If CurrentPosition > 0 Then Gfx.DrawRectangle(New Pen(Color.Red, 2), CurrentPosition - 1, 1, 18, 18)

            'Draw end-of-contribs box
            If Edit Is NullEdit Then Gfx.FillRectangle(Brushes.Black, X, 2, 16, 16)
        End If
    End Sub

End Class
