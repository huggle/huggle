Class IntegerTextBox

    Inherits TextBox

    'Text box which restricts input to integer values

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)

        Dim Position As Integer = SelectionStart, i As Integer

        While i < Text.Length
            If Not Char.IsDigit(Text(i)) Then
                Text = Text.Remove(i, 1)
                Position -= 1
            Else
                i += 1
            End If
        End While

        SelectionStart = Position
    End Sub

    Public Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

    Public Property Value() As Integer
        Get
            If Text = "" Then Return 0 Else Return CInt(Text)
        End Get
        Set(ByVal value As Integer)
            MyBase.Text = CStr(value)
        End Set
    End Property

End Class
