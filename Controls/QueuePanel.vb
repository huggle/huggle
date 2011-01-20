Class QueuePanel

    Private Gfx As BufferedGraphics, CanRender As Boolean

    Private Sub QueuePanel_HandleCreated() Handles Me.HandleCreated
        CanRender = True
    End Sub

    Private Sub QueuePanel_HandleDestroyed() Handles Me.HandleDestroyed
        CanRender = False
    End Sub

    Public Sub Draw(ByVal Queue As Queue, ByRef Scroll As Integer)
        If Gfx Is Nothing Then Gfx = BufferedGraphicsManager.Current.Allocate(CreateGraphics, _
            New Rectangle(0, 0, Config.QueueWidth, MainForm.Height))

        Dim X, Y As Integer
        Dim Length As Integer = Math.Min(Queue.Edits.Count - 1, (Height \ 20) - 2)

        Gfx.Graphics.Clear(Color.FromKnownColor(KnownColor.Control))
        If Queue.Refreshing Then Count.Text = Msg("main-queue-query") _
            Else Count.Text = Msg("main-queue-count", CStr(Queue.Edits.Count))
            For i As Integer = 0 To Length Step +1
                If Scroll + i > Queue.Edits.Count - 1 Then
                    Scroll -= 1
                    Exit For
                End If

                Dim Edit As Edit = Queue.Edits(i + Scroll)
                Dim Name As String = Edit.Page.Name, Height As Integer = 30

                X = Config.QueueWidth - 24
                Y = (i * 20) + 19
                Dim Curr As Integer = 0
                'Truncate page name
                While Height > 20 And Curr < Misc.GlExcess
                    Height = CInt(Gfx.Graphics.MeasureString(Name, Font, Width - 36).Height)
                    If Height > 20 Then Name = Name.Substring(0, Name.Length - 1)
                    Curr = +1
                End While

                If Name.Length < Edit.Page.Name.Length Then Name &= "..."

                Dim SpaceBrush As Brush

                Select Case Edit.Page.Space
                    Case Space.Article : SpaceBrush = Brushes.White
                    Case Space.Talk : SpaceBrush = Brushes.PaleGreen
                    Case Space.User, Space.UserTalk : SpaceBrush = Brushes.LightSteelBlue
                    Case Space.Project, Space.ProjectTalk : SpaceBrush = Brushes.Thistle
                    Case Else : SpaceBrush = Brushes.NavajoWhite
                End Select

                Gfx.Graphics.FillRectangle(SpaceBrush, 2, Y - 1, Width - 4, 17)
                Gfx.Graphics.DrawRectangle(Pens.DarkGray, 2, Y - 1, Width - 4, 17)
                Gfx.Graphics.DrawString(Name, Font, Brushes.Black, 4, Y + 1)

                'Draw icon
                Try
                    Gfx.Graphics.DrawImage(Edit.Icon, X, Y)
                Catch ex As InvalidOperationException
                End Try

                Select Case Edit.WarningLevel
                    Case UserLevel.Warn1 : Gfx.Graphics.DrawString("!1", MainForm.Font, Brushes.Black, X + 2, Y + 1)
                    Case UserLevel.Warn2 : Gfx.Graphics.DrawString("!2", MainForm.Font, Brushes.Black, X + 2, Y + 1)
                    Case UserLevel.Warn3 : Gfx.Graphics.DrawString("!3", MainForm.Font, Brushes.Black, X + 2, Y + 1)
                    Case UserLevel.WarnFinal : Gfx.Graphics.DrawString("!4", MainForm.Font, Brushes.Black, X + 2, Y + 1)
                End Select

                If Edit.Assisted AndAlso Edit.Type = EditType.None AndAlso Edit.WarningLevel = UserLevel.None _
                    AndAlso Not Edit.Bot Then Gfx.Graphics.DrawString("*", New Font(FontFamily.GenericSansSerif, _
                    14, FontStyle.Regular), Brushes.Black, X + 2, Y + 2)
            Next i
        If CanRender Then Gfx.Render()
    End Sub

End Class
