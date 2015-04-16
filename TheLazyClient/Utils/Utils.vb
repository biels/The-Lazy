Imports System.Reflection
Imports System.ComponentModel

Module Utils
    Function ConvertToMediaColor(c As System.Drawing.Color) As System.Windows.Media.Color
        Return System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B)
    End Function
    Function ConvertToDrawingColor(c As System.Windows.Media.Color) As System.Drawing.Color
        Return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B)
    End Function
    Function ColorToInt(c As System.Drawing.Color) As Integer
        Return c.ToArgb()
    End Function
    Function ColorToInt(c As System.Windows.Media.Color) As Integer
        Return ColorToInt(ConvertToDrawingColor(c))
    End Function
    Private Function DrawingColorFromInt(c As Integer) As System.Drawing.Color
        Return System.Drawing.Color.FromArgb(c)
    End Function
    Function ColorFromInt(c As Integer) As System.Windows.Media.Color
        Return ConvertToMediaColor(DrawingColorFromInt(c))
    End Function
    Function FindItemContaining(items As IEnumerable, target As Object) As Object
        If items Is Nothing Then Return Nothing
        If target Is Nothing Then Return Nothing
        For Each item As Object In items
            If item.ToString().Contains(target.ToString) Then
                Return item
            End If
        Next
        ' Return null;
        Return Nothing
    End Function
    Public Function ColorForRating(r As Integer)
        Select Case r
            'Case Is <= 2
            '    Return  Brushes.Red
            Case Is <= 3
                Return Brushes.DarkRed
            Case Is <= 4
                Return Brushes.OrangeRed
            Case Is <= 5
                Return Brushes.DarkOrange
            Case Is <= 7
                Return Brushes.Orange
            Case Is <= 9
                Return Brushes.Green
            Case Is <= 10
                Return Brushes.DarkGreen
        End Select
        Return Brushes.Blue
    End Function
    Public Sub UpdateMainWindow()
        GetMainWindow.UpdateHeading()
    End Sub
    Public Function GetMainWindow() As MainWindowElements
        Return Application.Current.MainWindow
    End Function
    Public Function TimeAgo(dt As DateTime) As String
        Dim span As TimeSpan = DateTime.Now - dt
        If span.Days > 365 Then
            Dim years As Integer = (span.Days / 365)
            If span.Days Mod 365 <> 0 Then
                years += 1
            End If
            Return [String].Format("fa {0} {1}", years, If(years = 1, "any", "anys"))
        End If
        If span.Days > 30 Then
            Dim months As Integer = (span.Days / 30)
            If span.Days Mod 31 <> 0 Then
                months += 1
            End If
            Return [String].Format("fa {0} {1}", months, If(months = 1, "mes", "mesos"))
        End If
        If span.Days > 0 Then
            Return [String].Format("fa {0} {1}", span.Days, If(span.Days = 1, "dia", "dies"))
        End If
        If span.Hours > 0 Then
            Return [String].Format("fa {0} {1}", span.Hours, If(span.Hours = 1, "hora", "hores"))
        End If
        If span.Minutes > 0 Then
            Return [String].Format("fa {0} {1}", span.Minutes, If(span.Minutes = 1, "minut", "minuts"))
        End If
        If span.Seconds > 5 Then
            Return [String].Format("fa {0} segons", span.Seconds)
        End If
        If span.Seconds <= 5 Then
            Return "ara mateix"
        End If
        Return String.Empty
    End Function
    <System.Runtime.CompilerServices.Extension> _
    Public Function GetDescription(Of T As Structure)(enumerationValue As T) As String
        Dim type As Type = enumerationValue.[GetType]()
        If Not type.IsEnum Then
            Throw New ArgumentException("EnumerationValue must be of Enum type", "enumerationValue")
        End If

        'Tries to find a DescriptionAttribute for a potential friendly name
        'for the enum
        Dim memberInfo As MemberInfo() = type.GetMember(enumerationValue.ToString())
        If memberInfo IsNot Nothing AndAlso memberInfo.Length > 0 Then
            Dim attrs As Object() = memberInfo(0).GetCustomAttributes(GetType(DescriptionAttribute), False)

            If attrs IsNot Nothing AndAlso attrs.Length > 0 Then
                'Pull out the description value
                Return DirectCast(attrs(0), DescriptionAttribute).Description
            End If
        End If
        'If we have no description attribute, just return the ToString of the enum
        Return enumerationValue.ToString()

    End Function
    Public Function GetSizeReadable(i As Long) As String
        Dim sign As String = (If(i < 0, "-", ""))
        Dim readable As Double = (If(i < 0, -i, i))
        Dim suffix As String
        If i >= &H1000000000000000L Then
            ' Exabyte
            suffix = "EB"
            readable = CDbl(i >> 50)
        ElseIf i >= &H4000000000000L Then
            ' Petabyte
            suffix = "PB"
            readable = CDbl(i >> 40)
        ElseIf i >= &H10000000000L Then
            ' Terabyte
            suffix = "TB"
            readable = CDbl(i >> 30)
        ElseIf i >= &H40000000 Then
            ' Gigabyte
            suffix = "GB"
            readable = CDbl(i >> 20)
        ElseIf i >= &H100000 Then
            ' Megabyte
            suffix = "MB"
            readable = CDbl(i >> 10)
        ElseIf i >= &H400 Then
            ' Kilobyte
            suffix = "KB"
            readable = CDbl(i)
        Else
            ' Byte
            Return i.ToString(sign & Convert.ToString("0 B"))
        End If
        readable = readable / 1024

        Return Convert.ToString(sign & readable.ToString("0.### ")) & suffix
    End Function
End Module
