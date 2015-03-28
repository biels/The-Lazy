Public Class RatingControl
    
    Private Sub RatingControl_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'RenderNumbers()
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
                .Margin = New Thickness(30 * i - 38, 0, 0, 0)
                If sldrSlider.Value >= i Then
                    .FontSize = 18
                    Select Case i
                        'Case Is <= 2
                        '    .Foreground = Brushes.Red
                        Case Is <= 3
                            .Foreground = Brushes.DarkRed
                        Case Is <= 4
                            .Foreground = Brushes.OrangeRed
                        Case Is <= 5
                            .Foreground = Brushes.DarkOrange
                        Case Is <= 7
                            .Foreground = Brushes.Orange
                        Case Is <= 9
                            .Foreground = Brushes.Green
                        Case Is <= 10
                            .Foreground = Brushes.DarkGreen
                    End Select
                End If
                If Not (sldrSlider.Value >= i And sldrSlider.Value >= (i + 1)) Then
                    .FontWeight = FontWeights.Bold
                End If
            End With
            cnvCanvas.Children.Add(lbl)
        Next
    End Sub

    Private Sub Slider_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        RenderNumbers()
    End Sub
End Class
