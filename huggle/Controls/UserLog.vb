'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for
'last modified by Petrb

'Copyright (C) 2011 Huggle team

'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.

'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.

Class UserLog

    'Control for displaying user log

    Inherits ListView2

    Private _User As User

    Sub New()
        DoubleBuffered = True
        FullRowSelect = True
        View = Windows.Forms.View.Details
        GridLines = True
        HeaderStyle = ColumnHeaderStyle.Nonclickable
    End Sub

    Public Property User() As User
        Get
            Return _User
        End Get
        Set(ByVal value As User)
            _User = value
            RefreshLog()
        End Set
    End Property

    Public Sub RefreshLog()
        Clear()

        If User IsNot Nothing Then
            Columns.Add("", Width - 10)
            Items.Add(Msg("blocklog-progress"))

            Dim NewRequest As New BlockLogRequest
            NewRequest.User = User
            NewRequest.Start(AddressOf RequestDone)
        End If
    End Sub

    Private Sub RequestDone(ByVal Result As RequestResult)
        If Result.Error Then
            Clear()
            Columns.Add("", Width - 10)
            Items.Add(Result.ErrorMessage)

        ElseIf User.Blocks Is Nothing OrElse User.Blocks.Count = 0 Then
            Clear()
            Columns.Add("", Width - 10)
            Items.Add(Msg("blocklog-none", User.Name))

        Else
            Items.Clear()
            Columns.Clear()
            Columns.Add("Date", 105)
            Columns.Add("Action", 50)
            Columns.Add("Duration", 60)
            Columns.Add("Options", 70)
            Columns.Add("Admin", 90)
            Columns.Add("Comment", 100)

            For Each Item As Block In User.Blocks
                Dim NewItem As New ListViewItem
                NewItem.Text = Item.Time.ToShortDateString & " " & Item.Time.ToShortTimeString
                NewItem.SubItems.Add(Item.Action)
                NewItem.SubItems.Add(Item.Duration)
                NewItem.SubItems.Add(Item.Options)
                NewItem.SubItems.Add(Item.Admin.Name)
                NewItem.SubItems.Add(TrimSummary(Item.Comment))

                Items.Add(NewItem)
            Next Item
        End If
    End Sub

End Class
