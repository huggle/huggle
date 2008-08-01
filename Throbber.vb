Imports System.Drawing.Drawing2D

Class Throbber

    Public Active As Boolean

    Private WithEvents Timer As New Timer
    Private Gfx As BufferedGraphics, Value As Integer, CanRender As Boolean
    Private Brush1 As Brush = New Pen(Color.SteelBlue).Brush, Brush2 As Brush = New Pen(Color.LightSteelBlue).Brush

    Private Sub Throbber_HandleCreated() Handles Me.HandleCreated
        CanRender = True
    End Sub

    Private Sub Throbber_HandleDestroyed() Handles Me.HandleDestroyed
        CanRender = False
    End Sub

    Private Sub Throbber_Load() Handles Me.Load
        Gfx = BufferedGraphicsManager.Current.Allocate(CreateGraphics, DisplayRectangle)
        Gfx.Graphics.SmoothingMode = SmoothingMode.HighQuality

        Timer.Interval = 20
        Timer.Start()
    End Sub

    Private Sub Timer_Tick() Handles Timer.Tick
        If Active Then
            Value = (Value + 1) Mod ((Height - 1) * 2)

            For i As Integer = -(Width \ (Height - 1)) To (Width \ (Height - 1))
                Gfx.Graphics.FillRectangle(If(i Mod 2 = 0, Brush1, Brush2), _
                    Value + (i * (Height - 1)), 1, Height - 1, Height - 1)
            Next i

            Gfx.Graphics.DrawRectangle(Pens.DarkGray, 0, 0, Width - 1, Height - 1)
            Gfx.Render()
        Else
            Gfx.Graphics.Clear(BackColor)
            Gfx.Graphics.DrawRectangle(Pens.DarkGray, 0, 0, Width - 1, Height - 1)
            If CanRender Then Gfx.Render()
        End If
    End Sub

    Public Sub Start()
        Active = True
    End Sub

    Public Sub [Stop]()
        Active = False
    End Sub

End Class
