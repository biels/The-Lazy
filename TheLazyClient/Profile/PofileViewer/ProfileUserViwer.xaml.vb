Imports TheLazyClientMVVM

Class ProfileUserViwer

    Private Sub Page1_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub
    Private _ElementUser As Entities.UserEntity
    Property ElementUser As Entities.UserEntity
        Get
            Return _ElementUser
        End Get
        Set(ByVal value As Entities.UserEntity)
            _ElementUser = value
        End Set
    End Property
    Sub Init()

    End Sub
    Sub UpdateUI()
        lblUsername.Content = ElementUser.real_name + "[" + ElementUser.username + "]"

    End Sub
    Sub UpdateElementUser(username As String)
        _ElementUser = c.cache.user_cache.getUser(username)
    End Sub
End Class
