Public Class RatingControl
    Public value As Integer = -1
    Public Event ValueChanged(value As Double)
    Public Property Rating() As Integer
        Get
            Return value
        End Get
        Set(ByVal value As Integer)
            Me.value = value
            sldrSlider.Value = value
            RenderNumbers()
        End Set
    End Property
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
                .Width = (Me.Width / 9.65)
                '.Margin.Left = 
                Dim w As Double = .Width
                If w > 0 Then
                    Dim m = 0
                    If i = 10 Then
                        .Width *= 2
                        m = -6
                    End If
                    .Margin = New Thickness((w) * (i - 1) - 10 + m, 0, 0, 0)
                End If

                If value >= (i * 100) Then
                    .FontSize = 18
                    .Foreground = ColorForRating(i)
                End If
                If Not (sldrSlider.Value >= i And sldrSlider.Value >= (i + 1) * 100) Then
                    .FontWeight = FontWeights.Bold
                End If
                Dim minWidth As Integer = lbl.FontSize * (CType(lbl.Content, String).Length)
                If .Width < minWidth Then
                    .Width = minWidth
                End If
                If value = -1 Then
                    .FontSize = 16
                    .Foreground = Brushes.Gray
                End If
            End With
            cnvCanvas.Children.Add(lbl)
        Next
    End Sub
    Private Sub Slider_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        If Me.IsLoaded = False Then Exit Sub
        value = e.NewValue
        If (value / 100) = 10 Then
            value = 1000
        End If
        RenderNumbers()
        RaiseEvent ValueChanged(value)
    End Sub

    Private Sub RatingControl_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        RenderNumbers()
    End Sub
End Class
