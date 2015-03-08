Class MainWindow

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Worker.Work()

    End Sub

    Private Sub Image_MouseDown(sender As Object, e As MouseButtonEventArgs)

    End Sub

    Private Sub btnClose_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles btnClose.MouseDown
        Me.Close()
    End Sub

    Private Sub Image_MouseDown_1(sender As Object, e As MouseButtonEventArgs)

    End Sub

    Private Sub btnMinimize_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles btnMinimize.MouseDown
        Me.WindowState = Windows.WindowState.Minimized
    End Sub
End Class
