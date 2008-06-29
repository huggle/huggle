Class EditInfoPanel

    Private Buffer As BufferedGraphics

    Public Enum DisplayMode
        PageName
        UserName
    End Enum

    Private Sub EditInfo_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Buffer = BufferedGraphicsManager.Current.Allocate(CreateGraphics, DisplayRectangle)
    End Sub

    Public Sub SetEdit(ByVal Edit As Edit, ByVal Mode As DisplayMode)
        If Edit IsNot Nothing AndAlso Edit.Page IsNot Nothing AndAlso Edit.User IsNot Nothing Then
            If Mode = DisplayMode.PageName Then PageUser.Text = Edit.User.Name Else PageUser.Text = Edit.Page.Name
            Change.Text = CStr(IIf(Edit.Size > 0, "+", "")) & CStr(Edit.Size)
            Change.Visible = (Not Edit.Size = 0)
            Summary.Text = TrimSummary(Edit.Summary)

            Dim LocalTime As Date = Edit.Time.ToLocalTime

            Time.Text = CStr(LocalTime.Year) & "-" & CStr(LocalTime.Month).PadLeft(2, "0"c) & "-" & _
                CStr(LocalTime.Day).PadLeft(2, "0"c) & " " & CStr(LocalTime.Hour).PadLeft(2, "0"c) & ":" & _
                CStr(LocalTime.Minute).PadLeft(2, "0"c)
        Else
            Change.Text = ""
            Summary.Text = ""
            Time.Text = ""
            PageUser.Text = ""
        End If
    End Sub

    Private Sub EditInfo_Paint(ByVal s As Object, ByVal e As PaintEventArgs) Handles Me.Paint
        Buffer.Graphics.Clear(Color.FromKnownColor(KnownColor.Control))
        Buffer.Graphics.DrawRectangle(Pens.DarkGray, 0, 0, Width - 1, Height - 2)
        Buffer.Render()
    End Sub

End Class
