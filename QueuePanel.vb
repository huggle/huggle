Class QueuePanel

    Private Gfx As BufferedGraphics, CanRender As Boolean

    Private Sub QueuePanel_HandleCreated() Handles Me.HandleCreated
        CanRender = True
    End Sub

    Private Sub QueuePanel_HandleDestroyed() Handles Me.HandleDestroyed
        CanRender = False
    End Sub

    Public Sub Draw(ByVal Queue As List(Of Edit))
        If Gfx Is Nothing Then Gfx = BufferedGraphicsManager.Current.Allocate(CreateGraphics, _
            New Rectangle(0, 0, QueueWidth, MainForm.Height))

        Dim X, Y As Integer
        Dim Length As Integer = Math.Min(Queue.Count - 1, (Height \ 20) - 2)

        Gfx.Graphics.Clear(Color.FromKnownColor(KnownColor.Control))
        Count.Text = CStr(Queue.Count) & " items"

        For i As Integer = 0 To Length
            If MainForm.QueueScroll.Value + i > Queue.Count - 1 Then
                MainForm.QueueScroll.Value -= 1
                Exit For
            End If

            Dim ThisEdit As Edit = Queue(i + MainForm.QueueScroll.Value)
            Dim Name As String = ThisEdit.Page.Name, Height As Integer = 30

            X = QueueWidth - 20
            Y = (i * 20) + 19

            'Truncate page name
            While Height > 20
                Height = CInt(Gfx.Graphics.MeasureString(Name, Font, Width - 36).Height)
                If Height > 20 Then Name = Name.Substring(0, Name.Length - 1)
            End While

            If Name.Length < ThisEdit.Page.Name.Length Then Name &= "..."

            Gfx.Graphics.FillRectangle(Brushes.White, 2, Y - 1, Width - 6, 17)
            Gfx.Graphics.DrawRectangle(Pens.DarkGray, 2, Y - 1, Width - 6, 17)
            Gfx.Graphics.DrawString(Name, Font, Brushes.Black, 4, Y + 1)

            'Draw icon
            Select Case ThisEdit.Type
                Case EditType.Blanked : Gfx.Graphics.DrawImage(My.Resources.blob_blanked, X, Y)
                Case EditType.ReplacedWith : Gfx.Graphics.DrawImage(My.Resources.blob_replaced, X, Y)
                Case EditType.Redirect : Gfx.Graphics.DrawImage(My.Resources.blob_redirect, X, Y)
                Case EditType.Revert : Gfx.Graphics.DrawImage(My.Resources.blob_revert, X, Y)
                Case EditType.Report : Gfx.Graphics.DrawImage(My.Resources.blob_report, X, Y)
                Case EditType.Message : Gfx.Graphics.DrawImage(My.Resources.blob_message, X, Y)
                Case EditType.Tag : Gfx.Graphics.DrawImage(My.Resources.blob_tag, X, Y)

                Case EditType.Warning
                    Gfx.Graphics.DrawImage(My.Resources.blob_blank, X, Y)

                    Select Case ThisEdit.WarningLevel
                        Case UserL.Warning : Gfx.Graphics.DrawImage(My.Resources.blob_warning, X, Y)
                        Case UserL.Warn1 : Gfx.Graphics.DrawString("!1", MainForm.Font, Brushes.Black, X + 2, Y + 1)
                        Case UserL.Warn2 : Gfx.Graphics.DrawString("!2", MainForm.Font, Brushes.Black, X + 2, Y + 1)
                        Case UserL.Warn3 : Gfx.Graphics.DrawString("!3", MainForm.Font, Brushes.Black, X + 2, Y + 1)
                        Case UserL.WarnFinal : Gfx.Graphics.DrawString("!4", MainForm.Font, Brushes.Black, X + 2, Y + 1)
                        Case UserL.Blocked : Gfx.Graphics.DrawImage(My.Resources.blob_blocknote, X, Y)
                    End Select

                Case Else
                    If ThisEdit.User IsNot Nothing Then
                        If ThisEdit.User Is MyUser Then
                            Gfx.Graphics.DrawImage(My.Resources.blob_me, X, Y)

                        ElseIf ThisEdit.User.Bot Then
                            Gfx.Graphics.DrawImage(My.Resources.blob_bot, X, Y)

                        Else
                            Select Case ThisEdit.User.Level
                                Case UserL.Ignore : Gfx.Graphics.DrawImage(My.Resources.blob_ignored, X, Y)
                                Case UserL.Blocked : Gfx.Graphics.DrawImage(My.Resources.blob_blocked, X, Y)
                                Case UserL.ReportedAIV : Gfx.Graphics.DrawImage(My.Resources.blob_reported, X, Y)
                                Case UserL.Warn1 : Gfx.Graphics.DrawImage(My.Resources.blob_warn_1, X, Y)
                                Case UserL.Warn2 : Gfx.Graphics.DrawImage(My.Resources.blob_warn_2, X, Y)
                                Case UserL.Warn3 : Gfx.Graphics.DrawImage(My.Resources.blob_warn_3, X, Y)
                                Case UserL.WarnFinal : Gfx.Graphics.DrawImage(My.Resources.blob_warn_4, X, Y)
                                Case UserL.Reverted : Gfx.Graphics.DrawImage(My.Resources.blob_reverted, X, Y)
                                Case Else : If ThisEdit.User.Anonymous _
                                    Then Gfx.Graphics.DrawImage(My.Resources.blob_anon, X, Y) _
                                    Else Gfx.Graphics.DrawImage(My.Resources.blob_none, X, Y)
                            End Select
                        End If
                    End If
            End Select
        Next i

        If CanRender Then Gfx.Render()
    End Sub

End Class
