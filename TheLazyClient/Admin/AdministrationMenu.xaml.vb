Public Class AdministrationMenu
  
    Private Sub AdministrationMenu_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        UpdateUI()
    End Sub
    Sub UpdateUI()
        lblPermissionLevel.Content = c.localUser.permission_level
        lblPermissionLevelRank.Content = PermissionManager.GetLevelDisplayName(PermissionManager.GetLevel(c.localUser.permission_level))
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If Not PermissionManager.IsAbleTo(PermissionManager.PermissionLevels.Moderator) Then Exit Sub
        Dim frm As New SubjectEditor
        frm.Show()
    End Sub
End Class
