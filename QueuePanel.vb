Class QueuePanel

    Private Buffer As BufferedGraphics

    Private Sub QueuePanel_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Buffer = BufferedGraphicsManager.Current.Allocate(CreateGraphics, _
            New Rectangle(0, 0, QueueWidth, Main.Height))
    End Sub

    Public Sub Draw(ByVal Queue As List(Of Edit))
        If Buffer Is Nothing Then Exit Sub
        Dim Gfx As Graphics = Buffer.Graphics, X, Y As Integer
        Dim Length As Integer = Math.Min(Queue.Count - 1, (Height \ 20) - 2)

        Gfx.Clear(Color.FromKnownColor(KnownColor.Control))
        Count.Text = CStr(Queue.Count) & " items"

        'Exit Sub

        For i As Integer = 0 To Length
            If Main.QueueScroll.Value + i > Queue.Count - 1 Then
                Main.QueueScroll.Value -= 1
                Exit For
            End If

            Dim ThisEdit As Edit = Queue(i + Main.QueueScroll.Value)
            Dim Name As String = ThisEdit.Page.Name, Height As Integer = 30

            X = QueueWidth - 20
            Y = (i * 20) + 19

            'Truncate page name
            While Height > 20
                Height = CInt(Gfx.MeasureString(Name, Font, Width - 36).Height)
                If Height > 20 Then Name = Name.Substring(0, Name.Length - 1)
            End While

            If Name.Length < ThisEdit.Page.Name.Length Then Name &= "..."

            Gfx.FillRectangle(Brushes.White, 2, Y - 1, Width - 6, 17)
            Gfx.DrawRectangle(Pens.DarkGray, 2, Y - 1, Width - 6, 17)
            Gfx.DrawString(Name, Font, Brushes.Black, 4, Y + 1)

            'Draw icon
            Select Case ThisEdit.Type
                Case EditType.Blanked : Gfx.DrawImage(My.Resources.blob_blanked, X, Y)
                Case EditType.ReplacedWith : Gfx.DrawImage(My.Resources.blob_replaced, X, Y)
                Case EditType.Redirect : Gfx.DrawImage(My.Resources.blob_redirect, X, Y)
                Case EditType.Revert : Gfx.DrawImage(My.Resources.blob_revert, X, Y)
                Case EditType.Report : Gfx.DrawImage(My.Resources.blob_report, X, Y)
                Case EditType.Message : Gfx.DrawImage(My.Resources.blob_message, X, Y)
                Case EditType.Tag : Gfx.DrawImage(My.Resources.blob_tag, X, Y)

                Case EditType.Warning
                    Gfx.DrawImage(My.Resources.blob_blank, X, Y)

                    Select Case ThisEdit.WarningLevel
                        Case UserL.Warning : Gfx.DrawImage(My.Resources.blob_warning, X, Y)
                        Case UserL.Warn1 : Gfx.DrawString("!1", Main.Font, Brushes.Black, X + 2, Y + 1)
                        Case UserL.Warn2 : Gfx.DrawString("!2", Main.Font, Brushes.Black, X + 2, Y + 1)
                        Case UserL.Warn3 : Gfx.DrawString("!3", Main.Font, Brushes.Black, X + 2, Y + 1)
                        Case UserL.WarnFinal : Gfx.DrawString("!4", Main.Font, Brushes.Black, X + 2, Y + 1)
                        Case UserL.Blocked : Gfx.DrawImage(My.Resources.blob_blocknote, X, Y)
                    End Select

                Case Else
                    If ThisEdit.User IsNot Nothing Then
                        If ThisEdit.User Is MyUser Then
                            Gfx.DrawImage(My.Resources.blob_me, X, Y)

                        ElseIf ThisEdit.User.Bot Then
                            Gfx.DrawImage(My.Resources.blob_bot, X, Y)

                        Else
                            Select Case ThisEdit.User.Level
                                Case UserL.Ignore : Gfx.DrawImage(My.Resources.blob_ignored, X, Y)
                                Case UserL.Blocked : Gfx.DrawImage(My.Resources.blob_blocked, X, Y)
                                Case UserL.ReportedAIV : Gfx.DrawImage(My.Resources.blob_reported, X, Y)
                                Case UserL.Warn1 : Gfx.DrawImage(My.Resources.blob_warn_1, X, Y)
                                Case UserL.Warn2 : Gfx.DrawImage(My.Resources.blob_warn_2, X, Y)
                                Case UserL.Warn3 : Gfx.DrawImage(My.Resources.blob_warn_3, X, Y)
                                Case UserL.WarnFinal : Gfx.DrawImage(My.Resources.blob_warn_4, X, Y)
                                Case UserL.Reverted : Gfx.DrawImage(My.Resources.blob_reverted, X, Y)
                                Case Else : If ThisEdit.User.Anonymous _
                                    Then Gfx.DrawImage(My.Resources.blob_anon, X, Y) _
                                    Else Gfx.DrawImage(My.Resources.blob_none, X, Y)
                            End Select
                        End If
                    End If
            End Select
        Next i

        Buffer.Render()
    End Sub

End Class
