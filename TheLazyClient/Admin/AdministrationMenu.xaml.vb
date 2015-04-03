Public Class AdministrationMenu
    WithEvents dispatcherTimer As New Windows.Threading.DispatcherTimer()
    Private Sub AdministrationMenu_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dispatcherTimer.IsEnabled = True
        dispatcherTimer.Interval = New TimeSpan(0, 0, 1)
        UpdateUI()
    End Sub
    Sub UpdateUI()
        lblPermissionLevel.Content = c.localUser.permission_level
        lblPermissionLevelRank.Content = PermissionManager.GetLevelDisplayName(PermissionManager.GetLevel(c.localUser.permission_level))
        chkCachingEnabked.IsChecked = c.config.caching_enabled
        lblSqlQueries.Content = String.Format("Consultes SQL: {0}, Memòria cau: {1}, Rendiment: {2}%", c.sql_query_count, c.cached_query_count, Int(c.cached_query_count / (c.sql_query_count + c.cached_query_count) * 100))

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If Not PermissionManager.IsAbleTo(PermissionManager.PermissionLevels.Moderator) Then Exit Sub
        Dim frm As New SubjectEditor
        frm.Show()
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        UpdateUI()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As RoutedEventArgs) Handles btnReset.Click
        c.sql_query_count = 0
        c.cached_query_count = 0
        UpdateUI()
    End Sub


  
    Private Sub chkCachingEnabked_Click(sender As Object, e As RoutedEventArgs) Handles chkCachingEnabked.Click
        c.config.caching_enabled = chkCachingEnabked.IsChecked
        chkCachingEnabked.IsChecked = c.config.caching_enabled
        UpdateUI()
    End Sub

    Private Sub dispatcherTimer_Tick(sender As Object, e As EventArgs) Handles dispatcherTimer.Tick
        UpdateUI()
    End Sub
End Class
