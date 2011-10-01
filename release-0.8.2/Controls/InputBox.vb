Class InputBox

    Private Sub InputBox_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Icon = My.Resources.icon_red_button
    End Sub

    Private Sub InputBox_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub OK_Click() Handles OK.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Value_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Value.KeyDown
        If e.KeyCode = Keys.Enter Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub

    Public Overloads Shared Function Show(ByVal Message As String) As String
        Return Show(Message, "")
    End Function

    Public Overloads Shared Function Show(ByVal Message As String, ByVal DefaultValue As String) As String
        Dim NewForm As New InputBox
        NewForm.Message.Text = Message
        NewForm.Value.Text = DefaultValue
        NewForm.Value.Focus()
        NewForm.Value.SelectAll()
        If NewForm.ShowDialog() = DialogResult.OK Then Return NewForm.Value.Text Else Return Nothing
    End Function

End Class
