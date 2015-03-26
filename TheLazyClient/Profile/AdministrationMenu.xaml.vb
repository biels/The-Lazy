Public Class AdministrationMenu
  
    Private Sub AdministrationMenu_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        UpdateUI()
    End Sub
    Sub UpdateUI()
        lblPermissionLevel.Content = c.localUser.permission_level
        lblPermissionLevelRank.Content = PermissionManager.GetLevelDisplayName(PermissionManager.GetLevel(c.localUser.permission_level))
    End Sub

End Class
