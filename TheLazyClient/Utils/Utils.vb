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
End Module
