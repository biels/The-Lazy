Public Class AdministrationMenu
    Sub UpdateUI()
        lblPermissionLevel.Content = c.localUser.permission_level

    End Sub
End Class
