Public Class StealthModeUI

    Private Sub StealthModeUI_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        UpdateCheckbox()
    End Sub
    Private Sub chkEnabled_Checked(sender As Object, e As RoutedEventArgs) Handles chkEnabled.Click
        UpdateCheckbox()
    End Sub
    Sub UpdateCheckbox()
        Dim enb As Boolean = chkEnabled.IsChecked
        chkEnabled.Foreground = If(enb, Brushes.Green, Brushes.IndianRed)
        chkEnabled.Content = If(enb, "Activat", "Desactivat")
    End Sub

End Class
