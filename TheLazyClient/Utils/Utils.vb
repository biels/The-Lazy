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
        Dim w As MainWindow = Application.Current.MainWindow
        w.UpdateHeading()
    End Sub
End Module
