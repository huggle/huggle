Imports System
Imports System.Drawing
Imports System.Globalization

Namespace Anole
    ''' <summary>
    ''' Summary description for HTMLColorParser.
    ''' </summary>
    Public Class HTMLColorParser
        Public Sub New()
        End Sub
        
        ''' <summary>
        ''' Given a color in text-name format (like red) or a color
        ''' in HTML color code format (like #ff0011) returns
        ''' a Color or Color.White
        ''' </summary>
        ''' <param name="scolor"></param>
        ''' <returns></returns>
        Public Shared Function ParseColor(scolor As String) As Color
            'Is it an HTML color?
            If scolor.StartsWith("#") Then
                Return HTMLColorParser.ColorFromHTMLColor(scolor)
            End If
            
            'A "standard" color?
            Select Case scolor.ToLower()
                Case "red"
                    Return Color.Red
                Case "green"
                    Return Color.Green
                Case "blue"
                    Return Color.Blue
                Case "purple"
                    Return Color.Purple
                Case "yellow"
                    Return Color.Yellow
                Case "pink"
                    Return Color.Pink
                Case "black"
                    Return Color.Black
                Case "gray"
                    Return Color.Gray
                Case "brown"
                    Return Color.Brown
                Case "orange"
                    Return Color.Orange
            End Select
            Return Color.White
            
        End Function
        
        ''' <summary>
        ''' Given a color in this format "#FF0001" returns
        ''' Color(255,0,1);
        ''' </summary>
        ''' <param name="scolor"></param>
        ''' <returns></returns>
        Protected Shared Function ColorFromHTMLColor(scolor As String) As Color
            
            If scolor.Length <> 7 Then
                Return Color.White
            End If
            Return Color.FromArgb(Int32.Parse(scolor.Substring(1, 2), NumberStyles.HexNumber), Int32.Parse(scolor.Substring(3, 2), NumberStyles.HexNumber), Int32.Parse(scolor.Substring(5, 2), NumberStyles.HexNumber))
        End Function
    End Class
End Namespace
