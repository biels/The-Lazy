Class MainWindow

    Private Sub btnClose_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles btnClose.MouseDown
        Me.Close()
    End Sub

    Private Sub btnMinimize_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles btnMinimize.MouseDown
        Me.WindowState = Windows.WindowState.Minimized
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        ' Worker.Work()
        ' MsgBox("Funció no implementada")
        Dim f As New SelectorConnexions.frmOptions
        f.ShowDialog()
    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        DragMove()
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim frm As New Login
        frm.ShowDialog()
    End Sub

    Private Sub MainWindow_ManipulationStarting(sender As Object, e As ManipulationStartingEventArgs) Handles Me.ManipulationStarting

    End Sub
End Class
