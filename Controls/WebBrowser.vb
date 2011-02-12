Imports System.IO
Imports System.Text.Encoding

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

Class WebBrowser
    Inherits System.Windows.Forms.WebBrowser

    Private Shared TempFiles As New Dictionary(Of String, String)
    Private Shared MD5 As System.Security.Cryptography.MD5 = Security.Cryptography.MD5.Create

    Public Overloads Property DocumentText() As String
        Get
            Return MyBase.DocumentText
        End Get
        Set(ByVal value As String)
            If Mono() Then SetBrowserTextTheHardWay(value) Else MyBase.DocumentText = value
        End Set
    End Property

    'Mono does not implement the DocumentText property
    'So we save the text we want to display to a file and have the browser load that file instead
    Private Sub SetBrowserTextTheHardWay(ByVal Text As String)

        Dim Hash As String = MD5.ComputeHash(UTF8.GetBytes(Text)).ToString

        If Not TempFiles.ContainsKey(Hash) Then
            Dim TempFileName As String = Path.GetTempFileName & ".html"
            File.WriteAllText(TempFileName, Text)
            TempFiles.Add(Hash, TempFileName)
        End If

        Navigate("file:///" & TempFiles(Hash))
    End Sub

    Public Sub Cancel()
        Me.Stop()
    End Sub

    Public Shared Sub ClearTempFiles()
        For Each Item As String In TempFiles.Values
            If File.Exists(Item) Then File.Delete(Item)
            If File.Exists(Path.GetFileNameWithoutExtension(Item)) _
                Then File.Delete(Path.GetFileNameWithoutExtension(Item))
        Next Item
    End Sub
End Class
