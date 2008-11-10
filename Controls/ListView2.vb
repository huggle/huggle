Class ListView2

    Inherits ListView

    Sub New()
        DoubleBuffered = True
        FullRowSelect = True
        View = Windows.Forms.View.Details
        GridLines = True
        HeaderStyle = ColumnHeaderStyle.Nonclickable
    End Sub

End Class
