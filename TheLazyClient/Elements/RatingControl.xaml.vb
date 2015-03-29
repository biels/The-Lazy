Public Class RatingControl
    
    Private Sub RatingControl_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        RenderNumbers()
    End Sub
    Sub RenderNumbers()
        If cnvCanvas Is Nothing Then Exit Sub
        'grdNumberGrid = New Grid
        cnvCanvas.Children.Clear()
        For i As Integer = 1 To 10 Step 1
            Dim lbl As New Label
            With lbl
                .Content = i
                .Height = 30
                .Width = (Me.Width / 10)
                '.Margin.Left = 
                Dim w As Double = .Width
                If w > 0 Then
                    .Margin = New Thickness((w) * (i - 1) - 10, 0, 0, 0)
                End If

                If sldrSlider.Value >= i Then
                    .FontSize = 18
                    .Foreground = ColorForI(i)
                End If
                If Not (sldrSlider.Value >= i And sldrSlider.Value >= (i + 1)) Then
                    .FontWeight = FontWeights.Bold
                End If
            End With
            cnvCanvas.Children.Add(lbl)
        Next
    End Sub
    Public Function ColorForI(i As Integer)
        Select Case i
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
    Private Sub Slider_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        RenderNumbers()
    End Sub

    Private Sub RatingControl_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        RenderNumbers()
    End Sub
End Class
