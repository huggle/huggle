'This is a source code or part of Huggle project
'revertrequests.vb
'This file contains code for listactions form
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
Imports System.Text.RegularExpressions

Class ListActionsForm

    Public List As List(Of String), Form As ListForm

    Private Sub ListFiltersForm_Load() Handles Me.Load
        Icon = My.Resources.huggle_icon
        NamespaceTransformSelector.SelectedIndex = 0

        For Each Item As Space In Space.All
            Namespaces.Items.Add(Item, Form.Spaces.Contains(Item))
        Next Item
    End Sub

    Private Sub ListFiltersForm_KeyDown(ByVal s As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub

    Private Sub NamespaceTransform_Click() Handles NamespaceTransform.Click
        Dim Changes, i As Integer

        While i < List.Count
            Dim Page As Page = GetPage(List(i))

            If NamespaceTransformSelector.SelectedIndex = 0 _
                Then ChangeTitle(Not Page.IsTalkPage, Page.TalkPageName, i, Changes) _
                Else ChangeTitle(Page.IsTalkPage, Page.SubjectPageName, i, Changes)
        End While

        List.Sort()

        MessageBox.Show(CStr(Changes) & " titles changed.", "Huggle", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ChangeTitle(ByVal Change As Boolean, ByVal NewTitle As String, _
        ByRef i As Integer, ByRef Changes As Integer)

        'Helper subroutine for above
        If Change Then
            List.RemoveAt(i)
            If Not List.Contains(NewTitle) Then List.Add(NewTitle)
            Changes += 1
        Else
            i += 1
        End If
    End Sub

    Private Sub OK_Click() Handles OK.Click
        Form.TitleRegex = TitleRegex.Regex
        Form.Spaces.Clear()

        For Each Item As Space In Namespaces.CheckedItems
            Form.Spaces.Add(Item)
        Next Item

        Close()
    End Sub

    Private Sub CheckAll_Click() Handles CheckAll.Click
        Dim All As Boolean = (Namespaces.CheckedIndices.Count = 0)

        For i As Integer = 0 To Namespaces.Items.Count - 1
            Namespaces.SetItemChecked(i, All)
        Next i
    End Sub

    Private Sub Apply_Click() Handles Apply.Click
        Dim i As Integer, Changes As Integer

        While i < List.Count - 1
            If (TitleRegex.Regex IsNot Nothing AndAlso Not TitleRegex.Regex.IsMatch(List(i))) _
                OrElse Not Namespaces.CheckedItems.Contains(GetPage(List(i)).Space) Then

                List.RemoveAt(i)
                Changes += 1
            Else
                i += 1
            End If
        End While

        Form.ListPages.BeginUpdate()
        Form.ListPages.Items.Clear()
        Form.ListPages.Items.AddRange(List.ToArray)
        Form.ListPages.EndUpdate()

        If Changes > 0 Then MessageBox.Show(CInt(Changes) & " pages removed.", "Huggle", _
            MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class