Public Class UserProfileViewer
    Private _User As TheLazyClientMVVM.Entities.UserEntity
    Public Property User() As TheLazyClientMVVM.Entities.UserEntity
        Get
            Return _User
        End Get
        Set(ByVal value As TheLazyClientMVVM.Entities.UserEntity)
            _User = value
            UpdateUI()
        End Set
    End Property

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        'MsgBox("Funció no implementada")
        Process.Start(String.Format("mailto:{0}", User.email))
    End Sub
    Sub UpdateUI()
        Title = User.real_name
        lblUsername.Content = String.Format("{0} ({1})", User.real_name, User.username)
        lblEmail.Content = User.email
        lblStatus.Content = User.status
        If User IsNot Nothing And User.education_center IsNot Nothing And User.academic_level IsNot Nothing Then
            lblEduactionCenter.Content = If(User.education_center.name, "<No especificat>")
            lblGroupCode.Content = If(User.group_code, "<No especificat>")
            lblLevel.Content = If(User.academic_level.name, "<No especificat>")
            lblLocation.Content = If(User.education_center.location, "<No especificat>")
        Else
            lblEduactionCenter.Content = "[Desconegut]"
            lblGroupCode.Content = "[-]"
            lblLevel.Content = "[-]"
            lblLocation.Content = "[-]"
        End If
       
    End Sub
End Class
