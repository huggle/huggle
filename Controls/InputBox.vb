Class InputBox

    Private Sub OK_Click() Handles OK.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click() Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Public Overloads Shared Function Show(ByVal Message As String) As String
        Return Show(Message, "")
    End Function

    Public Overloads Shared Function Show(ByVal Message As String, ByVal DefaultValue As String) As String
        Dim NewForm As New InputBox
        NewForm.Message.Text = Message
        NewForm.Value.Text = DefaultValue
        If NewForm.ShowDialog() = DialogResult.OK Then Return NewForm.Value.Text Else Return Nothing
    End Function

End Class
