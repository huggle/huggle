Class EditInfoPanel

    Private Buffer As BufferedGraphics

    Public Enum DisplayMode
        : PageName : UserName
    End Enum

    Private Sub EditInfo_Load() Handles Me.Load
        Buffer = BufferedGraphicsManager.Current.Allocate(CreateGraphics, DisplayRectangle)
    End Sub

    Public Sub SetEdit(ByVal Edit As Edit, ByVal Mode As DisplayMode)
        If Edit IsNot Nothing AndAlso Edit.Page IsNot Nothing AndAlso Edit.User IsNot Nothing Then
            If Mode = DisplayMode.PageName Then PageUser.Text = Edit.User.Name Else PageUser.Text = Edit.Page.Name
            Change.Text = CStr(If(Edit.Size > 0, "+", "")) & CStr(Edit.Size)
            Change.Visible = (Not Edit.Size = 0)
            If Edit.Summary Is Nothing Then Summary.Text = "" Else Summary.Text = TrimSummary(Edit.Summary)
            Time.Text = Timestamp(Edit.Time.ToLocalTime)
        Else
            Change.Text = ""
            Summary.Text = ""
            Time.Text = ""
            PageUser.Text = ""
        End If
    End Sub

    Private Sub EditInfo_Paint() Handles Me.Paint
        Buffer.Graphics.Clear(Color.FromKnownColor(KnownColor.Control))
        Buffer.Graphics.DrawRectangle(Pens.DarkGray, 0, 0, Width - 1, Height - 2)
        Buffer.Render()
    End Sub

End Class
