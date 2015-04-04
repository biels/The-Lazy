Module Utils
    Function ConvertToMediaColor(c As System.Drawing.Color) As System.Windows.Media.Color
        Return System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B)
    End Function
    Function ConvertToDrawingColor(c As System.Windows.Media.Color) As System.Drawing.Color
        Return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B)
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
    Public Function GetMainWindow() As MainWindow
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

End Module
