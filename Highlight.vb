Imports System.Threading
Imports System.Text

Module Highlight

    Public FontName As String = "Courier New"

    Public CommentC As Color = Color.FromArgb(255, 128, 128, 128)
    Public LinkC As Color = Color.FromArgb(255, 0, 0, 255)
    Public MagicWordC As Color = Color.FromArgb(255, 0, 128, 128)
    Public ExternalC As Color = Color.FromArgb(255, 0, 128, 208)
    Public TemplateC As Color = Color.FromArgb(255, 255, 0, 0)
    Public HtmlC As Color = Color.FromArgb(255, 160, 0, 160)
    Public ParamCallC As Color = Color.FromArgb(255, 160, 0, 0)
    Public ReferenceHC As Color = Color.FromArgb(255, 208, 255, 208)
    Public ImageHC As Color = Color.FromArgb(255, 216, 216, 255)
    Public ParameterHC As Color = Color.FromArgb(255, 255, 255, 160)
    Public ParamNameC As Color = Color.FromArgb(255, 128, 80, 0)

    Public ReadOnly Property RtfHeader() As String
        Get
            Return "{\rtf1{\fonttbl{\f0 " & FontName & ";}}{\colortbl ;" & _
                CS(CommentC) & CS(LinkC) & CS(MagicWordC) & CS(ExternalC) & CS(TemplateC) & CS(HtmlC) & _
                CS(ParamCallC) & CS(ReferenceHC) & CS(ImageHC) & CS(ParameterHC) & CS(ParamNameC) & "}\f0\fs20 "
        End Get
    End Property

    Public ReadOnly Property RtfFooter() As String
        Get
            Return "\par}"
        End Get
    End Property

    Private Function CS(ByVal Color As Color) As String
        Return "\red" & CStr(Color.R) & "\green" & CStr(Color.G) & "\blue" & CStr(Color.B) & ";"
    End Function

    Public Function RtfEscape(ByVal Text As String) As String
        Text = Text.Replace("\", "\\").Replace("{", "\{").Replace("}", "\}").Replace(LF, "\par ")

        Dim j As Integer = 0

        While j < Text.Length
            Dim a As Integer = Convert.ToInt32(Text(j))

            If a > 127 Then
                Text.Remove(j, 1)
                Text.Insert(j, "\u" & CStr(a).PadLeft(5, "0"c) & "?")
            End If

            j += 1
        End While

        Return Text
    End Function

    Class HighlightRequest

        Private _Done As HighlightCallback, _Text As String, Thread As Thread

        Private MagicWords() As String = {"ARTICLEPAGENAME", "ARTICLEPAGENAMEE", "ARTICLESPACE", "ARTICLESPACEE", _
            "BASEPAGENAME", "BASEPAGENAMEE", "CONTENTLANGUAGE", "CURRENTDAY", "CURRENTDAY2", "CURRENTDAYNAME", _
            "CURRENTDOW", "CURRENTHOUR", "CURRENTMONTH", "CURRENTMONTHABBREV", "CURRENTMONTHNAME", _
            "CURRENTMONTHNAMEGEN", "CURRENTTIME", "CURRENTTIMESTAMP", "CURRENTVERSION", "CURRENTWEEK", "CURRENTYEAR", _
            "DIRMARK", "DIRECTIONMARK", "DISPLAYTITLE", "FULLPAGENAME", "FULLPAGENAMEE", "LOCALDAY", "LOCALDAY2", _
            "LOCALDAYNAME", "LOCALDOW", "LOCALHOUR", "LOCALMONTH", "LOCALMONTHABBREV", "LOCALMONTHNAME", _
            "LOCALMONTHNAMEGEN", "LOCALTIME", "LOCALTIMESTAMP", "LOCALWEEK", "LOCALYEAR", "NAMESPACE", "NAMESPACEE", _
            "NUMBEROFADMINS", "NUMBEROFARTICLES", "NUMBEROFEDITS", "NUMBEROFFILES", "NUMBEROFUSERS", "PAGENAME", _
            "PAGENAMEE", "PAGESINCAT", "PAGESINCATEGORY", "REVISIONID", "REVISIONDAY", "REVISIONDAY", "REVISIONMONTH", _
            "REVISIONTIMESTAMP", "REVISIONYEAR", "SITENAME", "SCRIPTPATH", "SERVER", "SERVERNAME", "SUBJECTSPACE", _
            "SUBJECTSPACEE", "SUBPAGENAME", "SUBPAGENAMEE", "TALKPAGENAME", "TALKPAGENAMEE", "TALKSPACE", _
            "TALKSPACEE", "anchorencode", "filepath", "formatnum", "fullurl", "fullurle", "grammar", "int", "lc", _
            "lcfirst", "localurl", "localurle", "msg", "msgnw", "ns", "raw", "padleft", "padright", "plural", "uc", _
            "ucfirst"}

        Private Directives() As String = {"#REDIRECT", "__FORCETOC__", "__HIDDENCAT__", "__INDEX__", _
            "__NEWSECTIONLINK__", "__NOCC__", "__NOCONTENTCONVERT__", "__NOEDITSECTION__", "__NOGALLERY__", _
            "__NOHEADER__", "__NOINDEX__", "__NOTC__", "__NOTITLECONVERT__", "__NOTOC__", "__STATICREDIRECT__", _
            "__TOC__"}

        Public Delegate Sub HighlightCallback(ByVal Result As String)

        Private Class TemplateData

            Public Text As String
            Public Templates As New List(Of TemplateData)

            Sub New(ByVal Text As String)
                Me.Text = Text
            End Sub

        End Class

        Public Sub Start(ByVal Text As String, ByVal Done As HighlightCallback)
            _Done = Done
            _Text = Text

            Thread = New Thread(AddressOf HighlightThread)
            Thread.IsBackground = True
            Thread.Start()
        End Sub

        Public Sub Cancel()
            Thread.Abort()
        End Sub

        Private Sub HighlightThread()

            Dim Comment As New List(Of String), Nowiki As New List(Of String)
            Dim Templates As New List(Of TemplateData)
            Dim Open, Close As Integer
            Dim Source As New StringBuilder(_Text, _Text.Length * 3)

            Source.Replace("\", "\\").Replace("{", "\{").Replace("}", "\}")

            For Each Item As String In Directives
                Source = Source.Replace(Item, "{\cf3 " & Item & "}")
            Next Item

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("<!--", Close)
                If Open = -1 Then Exit While
                Close = Str.IndexOf("-->", Open)
                If Close = -1 Then Exit While

                Comment.Add("{\cf1 " & Str.Substring(Open, Close - Open + 3) & "}")

                Source.Remove(Open, Close - Open + 3)
                Source.Insert(Open, Convert.ToChar(0))
                Close = Open + 1
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("<nowiki>", Close)
                If Open = -1 Then Exit While
                Close = Str.IndexOf("</nowiki>", Open)
                If Close = -1 Then Exit While

                Nowiki.Add("{\cf0 " & Str.Substring(Open + 8, Close - Open - 8) & "}")

                Source.Remove(Open + 8, Close - Open - 8)
                Source.Insert(Open + 8, Convert.ToChar(1))
                Close = Open + 1
            End While

            Source.Replace("~~~~", "{\cf3\b ~~~~}")
            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("__", Close)
                If Open = -1 Then Exit While
                Close = Str.IndexOf("__", Open + 1)
                If Close = -1 Then Exit While

                Close += 10
                Source.Insert(Open, "{\cf3\b ")
                Source.Insert(Close, "}")
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("<u>", Close)
                If Open = -1 Then Exit While
                Close = Str.IndexOf("</u>", Open)
                If Close = -1 Then Exit While

                Close += 5
                Source.Insert(Open + 3, "{\ul ")
                Source.Insert(Close, "}")
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("<ref", Close)
                If Open = -1 Then Exit While

                Dim Close1 As Integer = Str.IndexOf("</ref>", Open), _
                    Close2 As Integer = Str.IndexOf("/>", Open), _
                    Close3 As Integer = Str.IndexOf(">", Open)

                If Close2 > -1 AndAlso Close2 = Close3 - 1 Then
                    Close = Close3 + 1
                ElseIf Close1 > -1 Then
                    Close = Close1 + 6
                Else
                    Exit While
                End If

                Close += 13
                Source.Insert(Open, "{\highlight8 ")
                Source.Insert(Close, "}")
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("[[", Close)
                If Open = -1 Then Exit While
                Close = Str.IndexOf("]]", Open)
                If Close = -1 Then Exit While

                Dim Position As Integer = Open + 1, Level As Integer = 1

                While Level > 0
                    Dim NextOpen As Integer = Str.IndexOf("[[", Position)
                    Dim NextClose As Integer = Str.IndexOf("]]", Position)

                    If NextOpen > -1 AndAlso NextOpen < NextClose Then
                        Level += 1
                        Position = NextOpen + 1
                    Else
                        Level -= 1
                        Position = NextClose + 1
                    End If
                End While

                If Position = 0 Then Exit While
                Close = Position - 1

                Dim LinkText As String = Str.Substring(Open + 2, Close - Open - 2)

                Source.Remove(Open, Close - Open + 2)

                If LinkText.StartsWith("Image:") Then
                    Dim Index As Integer = LinkText.IndexOf("|")

                    If Index > -1 Then
                        LinkText = "{\cf2 " & LinkText.Substring(0, Index) & "}{\b |}" & LinkText.Substring(Index + 1)
                    Else
                        LinkText = "{\cf2 " & LinkText & "}"
                    End If

                    Source.Insert(Open, "{\cf0\highlight9 [[" & LinkText & "]]}")
                    Close = Open + 20
                Else
                    Dim Index1 As Integer = LinkText.IndexOf("|")
                    Dim Index2 As Integer = LinkText.IndexOf("\{\{")

                    Dim Index As Integer = -1

                    If Index1 > -1 Then Index = Index1
                    If Index2 > -1 AndAlso Index2 < Index1 Then Index = Index2

                    If Index > -1 Then
                        LinkText = "{\cf2 " & LinkText.Substring(0, Index) & "}" & LinkText.Substring(Index)
                    Else
                        LinkText = "{\cf2 " & LinkText & "}"
                    End If

                    Source.Insert(Open, "{\cf0 [[}" & LinkText & "{\cf0 ]]}")
                    Close = Open + 7
                End If
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("http://", Close)
                If Open = -1 Then Exit While

                Close = Source.Length + 1

                Dim Close1 As Integer = Str.IndexOf(" ", Open)
                Dim Close2 As Integer = Str.IndexOf("|", Open)
                Dim Close3 As Integer = Str.IndexOf("}", Open)
                Dim Close4 As Integer = Str.IndexOf(LF, Open)

                If Close1 > -1 Then Close = Close1
                If Close2 > -1 AndAlso Close2 < Close Then Close = Close2
                If Close3 > -1 AndAlso Close3 < Close Then Close = Close3 - 1
                If Close4 > -1 AndAlso Close4 < Close Then Close = Close4

                If Close = Source.Length + 1 OrElse Close = -1 Then Exit While

                Dim Index As Integer = Str.Substring(Open, Close - Open).IndexOf(" ")
                Close += 6

                Source.Insert(Open, "{\cf4 ")
                If Index > -1 Then Source.Insert(Open + Index + 6, "}") Else Source.Insert(Close, "}")
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("'''''", Close)
                If Open = -1 Then Exit While

                Dim Close1 As Integer = Str.IndexOf(LF, Open + 2)
                Dim Close2 As Integer = Str.IndexOf("'''''", Open + 2) + 5
                Close = -1
                If Close1 > -1 Then Close = Close1
                If Close2 > -1 AndAlso (Close1 = -1 OrElse Close2 < Close1) Then Close = Close2
                If Close = -1 Then Exit While

                Close += 6
                Source.Insert(Open, "{\b\i ")
                Source.Insert(Close, "}")
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("'''", Close)
                If Open = -1 Then Exit While

                If Open < Source.Length - 3 AndAlso Source(Open + 3) = "'" Then
                    Close = Open + 3
                    Continue While
                End If

                Dim Close1 As Integer = Str.IndexOf(LF, Open + 3)
                Dim Close2 As Integer = Str.IndexOf("'''", Open + 3) + 3
                Close = -1
                If Close1 > -1 Then Close = Close1
                If Close2 > -1 AndAlso (Close1 = -1 OrElse Close2 < Close1) Then Close = Close2
                If Close = -1 Then Exit While

                Close += 4
                Source.Insert(Open, "{\b ")
                Source.Insert(Close, "}")
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("''", Close)
                If Open = -1 Then Exit While

                If Open < Source.Length - 2 AndAlso Source(Open + 2) = "'" Then
                    Close = Open + 2
                    Continue While
                End If

                Dim Close1 As Integer = Str.IndexOf(LF, Open + 2)
                Dim Close2 As Integer = Str.IndexOf("''", Open + 2) + 2
                Close = -1
                If Close1 > -1 Then Close = Close1
                If Close2 > -1 AndAlso (Close1 = -1 OrElse Close2 < Close1) Then Close = Close2
                If Close = -1 Then Exit While

                Close += 4
                Source.Insert(Open, "{\i ")
                Source.Insert(Close, "}")
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("\{\{\{", Close)
                If Open = -1 Then Exit While
                Close = Str.IndexOf("\}\}\}", Open)
                If Close = -1 Then Exit While

                If Str.Substring(Open + 6, 2) = "\{" Then Continue While

                Dim ParameterText As String = Str.Substring(Open + 6, Close - Open - 6)
                Dim Index As Integer = ParameterText.IndexOf("|")

                If Index > -1 Then ParameterText = ParameterText.Substring(0, Index) & "}{\b |}{" _
                    & ParameterText.Substring(Index + 1)
                ParameterText = "{\b0\cf0\highlight10 \{\{\{{\cf11\b " & ParameterText & "}\}\}\}}"

                Source.Remove(Open, Close - Open + 6)
                Source.Insert(Open, ParameterText)
                Close += 32
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("<", Close)
                If Open = -1 Then Exit While
                Close = Str.IndexOf(">", Open)
                If Close = -1 Then Exit While

                Dim HtmlText As String = Str.Substring(Open + 1, Close - Open - 1)
                Dim Index As Integer = HtmlText.IndexOf(" ")

                If Index > -1 Then HtmlText = HtmlText.Insert(Index, "}") Else HtmlText &= "}"

                HtmlText = "{\cf0\b0 <{\cf6\b " & HtmlText & ">}"

                Source.Remove(Open, Close - Open + 1)
                Source.Insert(Open, HtmlText)
                Close += 20
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                If Str.StartsWith("==") Then Open = 0 Else Open = Str.IndexOf(LF & "==", Close)
                If Open = -1 Then Exit While
                Close = Str.IndexOf("==", Open + 4)
                If Close = -1 Then Exit While

                Source.Insert(Open, "{\b ")
                Source.Insert(Close + 6, "}")
                Close += 7
            End While

            Close = 0

            While True
                Dim Str As String = Source.ToString
                Open = Str.IndexOf("\{\{", Close)
                If Open = -1 OrElse Open > Str.Length - 8 Then Exit While
                Close = Open + 4
                If Str.Substring(Open + 4, 2) = "\{" AndAlso Str.Substring(Open + 6, 2) <> "\{" Then Continue While

                Dim Level As Integer = 1

                While Level > 0
                    Dim NextOpen, NextClose As Integer
                    Dim ThisClose As Integer = Close

                    Do
                        NextOpen = Str.IndexOf("\{\{", ThisClose)
                        If NextOpen = -1 OrElse NextOpen > Str.Length - 8 Then Exit Do
                        If Not (Str.Substring(NextOpen + 4, 2) = "\{" AndAlso Str.Substring(NextOpen + 6, 2) <> "\{") _
                            Then Exit Do
                        ThisClose = NextOpen + 6
                    Loop

                    Do
                        NextClose = Str.IndexOf("\}\}", Close)
                        If NextClose = -1 OrElse NextClose > Str.Length - 8 Then Exit Do
                        If Not (Str.Substring(NextClose + 4, 2) = "\}" AndAlso Str.Substring(NextClose + 6, 2) <> "\}") _
                            Then Exit Do
                        Close = NextClose + 6
                    Loop

                    If NextOpen > -1 AndAlso NextOpen < NextClose Then
                        Level += 1
                        Close = NextOpen + 4
                    Else
                        Level -= 1
                        Close = NextClose + 4
                    End If
                End While

                Close -= 4
                If Close = -1 Then Exit While

                Dim TemplateText As String = Str.Substring(Open, Close - Open + 4)
                Dim TemplateItem As New TemplateData(TemplateText)
                Templates.Add(TemplateItem)
                ParseTemplate(TemplateItem)

                Source.Remove(Open, Close - Open + 4)
                Source.Insert(Open, Convert.ToChar(14))
                Close = Open + 1
            End While

            For Each Item As TemplateData In Templates
                UnpackTemplates(Item)
                Dim Index As Integer = Source.ToString.IndexOf(Convert.ToChar(14))

                If Index > -1 Then
                    Source.Remove(Index, 1)
                    Source.Insert(Index, Item.Text)
                End If
            Next Item

            Dim Lists() As List(Of String) = {Nowiki, Comment}

            For i As Integer = 0 To Lists.Length - 1
                For Each Item As String In Lists(i)
                    Dim Index As Integer = Source.ToString.IndexOf(Convert.ToChar(1 - i))

                    If Index > -1 Then
                        Source.Remove(Index, 1)
                        Source.Insert(Index, Item)
                    End If
                Next Item
            Next i

            Source.Replace(LF, "\par ")

            Dim j As Integer = 0

            While j < Source.Length
                Dim a As Integer = Convert.ToInt32(Source(j))

                If a > 127 Then
                    Source.Remove(j, 1)
                    Source.Insert(j, "\u" & CStr(a).PadLeft(5, "0"c) & "?")
                End If

                j += 1
            End While

            Source.Insert(0, RtfHeader)
            Source.Append(RtfFooter)

            SyncContext.Post(AddressOf HighlightDone, CObj(Source.ToString))
        End Sub

        Private Sub HighlightDone(ByVal SourceObject As Object)
            If _Done IsNot Nothing Then _Done(CStr(SourceObject))
        End Sub

        Private Sub ParseTemplate(ByVal Data As TemplateData)
            Dim Open, Close As Integer
            Close = 4

            While True
                Open = Data.Text.IndexOf("\{\{", Close)
                If Open = -1 OrElse Open > Data.Text.Length - 8 Then Exit While
                Close = Open + 4
                If Data.Text.Substring(Open + 4, 2) = "\{" AndAlso Data.Text.Substring(Open + 6, 2) <> "\{" _
                    Then Continue While

                Dim Level As Integer = 1

                While Level > 0
                    Dim NextOpen, NextClose As Integer
                    Dim ThisClose As Integer = Close

                    Do
                        NextOpen = Data.Text.IndexOf("\{\{", ThisClose)
                        If NextOpen = -1 OrElse NextOpen > Data.Text.Length - 8 Then Exit Do
                        If Not (Data.Text.Substring(NextOpen + 4, 2) = "\{" _
                            AndAlso Data.Text.Substring(NextOpen + 6, 2) <> "\{") Then Exit Do
                        ThisClose = NextOpen + 6
                    Loop

                    Do
                        NextClose = Data.Text.IndexOf("\}\}", Close)
                        If NextClose = -1 OrElse NextClose > Data.Text.Length - 8 Then Exit Do
                        If Not (Data.Text.Substring(NextClose + 4, 2) = "\}" _
                            AndAlso Data.Text.Substring(NextClose + 6, 2) <> "\}") Then Exit Do
                        Close = NextClose + 6
                    Loop

                    If NextOpen > -1 AndAlso NextOpen < NextClose Then
                        Level += 1
                        Close = NextOpen + 4
                    Else
                        Level -= 1
                        Close = NextClose + 4
                    End If
                End While

                Close -= 4
                If Close = -1 Then Exit While

                Dim TemplateText As String = Data.Text.Substring(Open, Close - Open + 4)
                Dim TemplateItem As New TemplateData(TemplateText)
                Data.Templates.Add(TemplateItem)
                ParseTemplate(TemplateItem)

                Data.Text = Data.Text.Substring(0, Open) & Convert.ToChar(14) & Data.Text.Substring(Close + 4)
                Close = Open + 1
            End While

            Data.Text = Data.Text.Substring(4, Data.Text.Length - 8)

            Dim Params As String() = Data.Text.Split("|"c)

            If Params(0).Contains(":") AndAlso (Params(0).Contains("#") OrElse Array.IndexOf(MagicWords, _
                Params(0).Substring(0, Params(0).IndexOf(":"))) > -1) Then

                Params(0) = "{\cf3\b " & Params(0).Replace(":", "\cf0 :") & "}"

            ElseIf Array.IndexOf(MagicWords, Params(0)) > -1 Then
                Params(0) = "{\cf3\b " & Params(0) & "}"

            ElseIf Params(0).Contains("subst:") Then
                Params(0) = Params(0).Replace("subst:", "{\b{\cf0 subst}:{\cf4 ") & "}}"

                Dim ValueIndex As Integer = Params(0).IndexOf("{\cf4 ")
                Dim Value As String = Params(0).Substring(Params(0).IndexOf("{\cf4 "))
                Dim Name As String = Value.Substring(5, Value.Length - 7).Replace(Convert.ToChar(3), "").Trim(" "c)

                If Value.Contains("#") AndAlso Value.Contains(":") Then
                    Dim Index As Integer = Value.IndexOf(":")
                    Value = "{\cf3 " & Value.Substring(6)
                    Value = Value.Substring(0, Index) & "}" & Value.Substring(Index, Value.Length - Index - 1)
                    Params(0) = Params(0).Substring(0, ValueIndex) & Value

                ElseIf Array.IndexOf(MagicWords, Name) > -1 Then
                    Value = "{\cf3 " & Value.Substring(6)
                    Params(0) = Params(0).Substring(0, ValueIndex) & Value
                End If

            Else
                Params(0) = "{\cf5\b " & Params(0) & "}"
            End If

            For i As Integer = 1 To Params.Length - 1
                Dim Index2 As Integer = Params(i).IndexOf("=")
                If Index2 > -1 Then Params(i) = "{\cf7 " & Params(i).Substring(0, Index2) & "}" _
                    & Params(i).Substring(Index2)
            Next i

            Data.Text = String.Join("{\b |}", Params)
            Data.Text = "{\b0\cf0 \{\{" & Data.Text & "\}\}}"
        End Sub

        Private Sub UnpackTemplates(ByVal Data As TemplateData)
            For i As Integer = 0 To Data.Templates.Count - 1
                UnpackTemplates(Data.Templates(i))

                Dim Index As Integer = Data.Text.IndexOf(Convert.ToChar(14))
                If Index > -1 Then Data.Text = Data.Text.Substring(0, Index) _
                    & Data.Templates(i).Text & Data.Text.Substring(Index + 1)
            Next i
        End Sub

    End Class

End Module
