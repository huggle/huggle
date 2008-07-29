Imports System.Drawing.Drawing2D

Class Throbber

    Public Active As Boolean
    Private WithEvents Timer As New Timer
    Private Gfx As BufferedGraphics, Value As Integer
    Private Brush1 As Brush = New Pen(Color.SteelBlue).Brush, Brush2 As Brush = New Pen(Color.LightSteelBlue).Brush

    Private Sub Throbber_Load() Handles Me.Load
        Gfx = BufferedGraphicsManager.Current.Allocate(CreateGraphics, DisplayRectangle)
        Gfx.Graphics.SmoothingMode = SmoothingMode.HighQuality

        Timer.Interval = 20
        Timer.Start()
    End Sub

    Private Sub Timer_Tick() Handles Timer.Tick
        If Me.Visible Then
            If Active Then
                Value = (Value + 1) Mod (Height * 2)

                For i As Integer = -(Width \ Height) To (Width \ Height)
                    Gfx.Graphics.FillRectangle(If(i Mod 2 = 0, Brush1, Brush2), Value + (i * Height), 0, Height, Height)
                Next i

                Gfx.Graphics.DrawRectangle(Pens.DarkGray, 0, 0, Width - 1, Height - 1)
                Gfx.Render()
            Else
                Gfx.Graphics.Clear(BackColor)
                Gfx.Graphics.DrawRectangle(Pens.DarkGray, 0, 0, Width - 1, Height - 1)
                Gfx.Render()
            End If
        End If
    End Sub

    Public Sub Start()
        Active = True
    End Sub

    Public Sub [Stop]()
        Active = False
    End Sub

End Class
