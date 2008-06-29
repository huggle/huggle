Module Drawing

    Public Function History(ByVal Page As Page) As Image
        If Main.History.Width < 0 Then Return Nothing
        Dim Result As New Bitmap(Main.History.Width, 20)
        Dim Gfx As Graphics = Graphics.FromImage(Result)

        Gfx.Clear(Color.FromKnownColor(KnownColor.Control))
        Gfx.DrawImage(My.Resources.gradient, 2, 2, Result.Width - 4, Result.Height - 4)
        Gfx.DrawRectangle(Pens.DarkGray, 1, 1, Result.Width - 3, Result.Height - 3)

        If Page IsNot Nothing Then
            Dim CurrentPosition As Integer = -1, OldPosition As Integer = -1
            Dim ThisEdit As Edit = Page.LastEdit
            Dim X As Integer = Result.Width - 18 + (HistoryOffset * 17)

            While ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
                If X < 0 Then Exit While

                If X < Result.Width Then
                    Select Case ThisEdit.Type
                        Case EditType.Blanked : Gfx.DrawImage(My.Resources.blob_blanked, X, 2)
                        Case EditType.ReplacedWith : Gfx.DrawImage(My.Resources.blob_replaced, X, 2)
                        Case EditType.Redirect : Gfx.DrawImage(My.Resources.blob_redirect, X, 2)
                        Case EditType.Revert : Gfx.DrawImage(My.Resources.blob_revert, X, 2)
                        Case EditType.Report : Gfx.DrawImage(My.Resources.blob_report, X, 2)
                        Case EditType.Message : Gfx.DrawImage(My.Resources.blob_message, X, 2)
                        Case EditType.Tag : Gfx.DrawImage(My.Resources.blob_tag, X, 2)

                        Case EditType.Warning
                            Gfx.DrawImage(My.Resources.blob_blank, X, 2)

                            Select Case ThisEdit.WarningLevel
                                Case UserL.Warning : Gfx.DrawImage(My.Resources.blob_warning, X, 2)
                                Case UserL.Warn1 : Gfx.DrawString("!1", Main.Font, Brushes.Black, X + 2, 3)
                                Case UserL.Warn2 : Gfx.DrawString("!2", Main.Font, Brushes.Black, X + 2, 3)
                                Case UserL.Warn3 : Gfx.DrawString("!3", Main.Font, Brushes.Black, X + 2, 3)
                                Case UserL.WarnFinal : Gfx.DrawString("!4", Main.Font, Brushes.Black, X + 2, 3)
                                Case UserL.Blocked : Gfx.DrawImage(My.Resources.blob_blocknote, X, 2)
                            End Select

                        Case Else
                            If ThisEdit.User IsNot Nothing Then
                                If ThisEdit.User Is MyUser Then
                                    Gfx.DrawImage(My.Resources.blob_me, X, 2)

                                ElseIf ThisEdit.User.Bot Then
                                    Gfx.DrawImage(My.Resources.blob_bot, X, 2)

                                Else
                                    Select Case ThisEdit.User.Level
                                        Case UserL.Ignore : Gfx.DrawImage(My.Resources.blob_ignored, X, 2)
                                        Case UserL.Blocked : Gfx.DrawImage(My.Resources.blob_blocked, X, 2)
                                        Case UserL.ReportedAIV : Gfx.DrawImage(My.Resources.blob_reported, X, 2)
                                        Case UserL.Warn1 : Gfx.DrawImage(My.Resources.blob_warn_1, X, 2)
                                        Case UserL.Warn2 : Gfx.DrawImage(My.Resources.blob_warn_2, X, 2)
                                        Case UserL.Warn3 : Gfx.DrawImage(My.Resources.blob_warn_3, X, 2)
                                        Case UserL.WarnFinal : Gfx.DrawImage(My.Resources.blob_warn_4, X, 2)
                                        Case UserL.Reverted : Gfx.DrawImage(My.Resources.blob_reverted, X, 2)
                                        Case Else : If ThisEdit.User.Anonymous _
                                            Then Gfx.DrawImage(My.Resources.blob_anon, X, 2) _
                                            Else Gfx.DrawImage(My.Resources.blob_none, X, 2)
                                    End Select
                                End If
                            End If
                    End Select

                    If ThisEdit.Id = CurrentEdit.Id Then
                        CurrentPosition = X - 1
                        If Not CurrentEdit.Multiple Then OldPosition = X - 18
                    End If

                    If ThisEdit.Page IsNot Nothing AndAlso ThisEdit Is ThisEdit.Page.LastEdit _
                        Then Gfx.DrawRectangle(Pens.DarkBlue, X, 2, 15, 15)
                    If CurrentEdit.Multiple AndAlso ThisEdit.Id = CurrentEdit.Oldid Then OldPosition = X - 1
                    Gfx.DrawLine(Pens.DarkGray, X + 16, 2, X + 16, 17)
                End If

                X -= 17
                ThisEdit = ThisEdit.Prev
            End While

            If X < 0 AndAlso ThisEdit IsNot Nothing AndAlso (ThisEdit Is CurrentEdit _
                OrElse ThisEdit.Id = CurrentEdit.Oldid) AndAlso Not (ThisEdit Is Page.LastEdit _
                OrElse ThisEdit Is Page.LastEdit.Prev) Then

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
            If ThisEdit Is NullEdit Then Gfx.FillRectangle(Brushes.Black, X, 2, 16, 16)
        End If

        Return Result
    End Function

    Public Function Contribs(ByVal User As User) As Image
        If Main.Contribs.Width < 0 Then Return Nothing
        Dim Result As New Bitmap(Main.Contribs.Width, 20)
        Dim Gfx As Graphics = Graphics.FromImage(Result)

        Gfx.Clear(Color.FromKnownColor(KnownColor.Control))
        Gfx.DrawImage(My.Resources.gradient, 2, 2, Result.Width - 4, Result.Height - 4)
        Gfx.DrawRectangle(Pens.DarkGray, 1, 1, Result.Width - 3, Result.Height - 3)

        If User IsNot Nothing Then
            Dim CurrentPosition As Integer = 0, DisableScroll As Boolean = True
            Dim ThisEdit As Edit = User.LastEdit
            Dim X As Integer = Result.Width - 18 + (ContribsOffset * 17)

            While ThisEdit IsNot Nothing AndAlso ThisEdit IsNot NullEdit
                If X < 0 Then Exit While

                If X < Result.Width Then

                    'Draw icon
                    Select Case ThisEdit.Type
                        Case EditType.Blanked : Gfx.DrawImage(My.Resources.blob_blanked, X, 2)
                        Case EditType.ReplacedWith : Gfx.DrawImage(My.Resources.blob_replaced, X, 2)
                        Case EditType.Redirect : Gfx.DrawImage(My.Resources.blob_redirect, X, 2)
                        Case EditType.Revert : Gfx.DrawImage(My.Resources.blob_revert, X, 2)
                        Case EditType.Report : Gfx.DrawImage(My.Resources.blob_report, X, 2)
                        Case EditType.Message : Gfx.DrawImage(My.Resources.blob_message, X, 2)
                        Case EditType.Tag : Gfx.DrawImage(My.Resources.blob_tag, X, 2)

                        Case EditType.Warning
                            Gfx.DrawImage(My.Resources.blob_blank, X, 2)

                            Select Case ThisEdit.WarningLevel
                                Case UserL.Warning : Gfx.DrawImage(My.Resources.blob_warning, X, 2)
                                Case UserL.Warn1 : Gfx.DrawString("!1", Main.Font, Brushes.Black, X + 2, 3)
                                Case UserL.Warn2 : Gfx.DrawString("!2", Main.Font, Brushes.Black, X + 2, 3)
                                Case UserL.Warn3 : Gfx.DrawString("!3", Main.Font, Brushes.Black, X + 2, 3)
                                Case UserL.WarnFinal : Gfx.DrawString("!4", Main.Font, Brushes.Black, X + 2, 3)
                                Case UserL.Blocked : Gfx.DrawImage(My.Resources.blob_blocknote, X, 2)
                            End Select

                        Case Else
                            If ThisEdit.User.Bot Then
                                Gfx.DrawImage(My.Resources.blob_bot, X, 2)
                            Else
                                Select Case ThisEdit.User.Level
                                    Case UserL.Ignore : Gfx.DrawImage(My.Resources.blob_ignored, X, 2)
                                    Case UserL.Blocked : Gfx.DrawImage(My.Resources.blob_blocked, X, 2)
                                    Case UserL.ReportedAIV : Gfx.DrawImage(My.Resources.blob_reported, X, 2)
                                    Case UserL.Warn1 : Gfx.DrawImage(My.Resources.blob_warn_1, X, 2)
                                    Case UserL.Warn2 : Gfx.DrawImage(My.Resources.blob_warn_2, X, 2)
                                    Case UserL.Warn3 : Gfx.DrawImage(My.Resources.blob_warn_3, X, 2)
                                    Case UserL.WarnFinal : Gfx.DrawImage(My.Resources.blob_warn_4, X, 2)
                                    Case UserL.Reverted : Gfx.DrawImage(My.Resources.blob_reverted, X, 2)
                                    Case Else : If ThisEdit.User.Anonymous _
                                        Then Gfx.DrawImage(My.Resources.blob_anon, X, 2) _
                                        Else Gfx.DrawImage(My.Resources.blob_none, X, 2)
                                End Select
                            End If
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
