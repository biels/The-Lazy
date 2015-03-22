Public Class ProfileEditor
    Private _SelectedGroup As TheLazyClientMVVM.Entities.GroupEntity
    Public Property SelectedGroup() As TheLazyClientMVVM.Entities.GroupEntity
        Get
            Return _SelectedGroup
        End Get
        Set(ByVal value As TheLazyClientMVVM.Entities.GroupEntity)
            _SelectedGroup = value
            If value IsNot Nothing Then lblGroupPreview.Text = value.ToString
        End Set
    End Property

    Private Sub btnSelectGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnSelectGroup.Click
        Dim frm As New GroupEditor
        frm.Init()
        frm.UpdateUI()
        frm.ShowDialog()
        If frm.SelectedGroup IsNot Nothing Then
            'Establir el grup seleccionat
            SelectedGroup = frm.SelectedGroup
        End If
    End Sub
    Sub LoadLocalValues()
        txtFullName.Text = c.localUser.real_name
        txtEmail.Text = c.localUser.email
        SelectedGroup = c.localUser.group
    End Sub
    Sub SaveChanges()
        TheLazyClientMVVM.DbClient.DbUserClient.updateUser(c.localUser.username, txtEmail.Text, txtFullName.Text, SelectedGroup.id)
    End Sub

    Private Sub btnSaveChanges_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveChanges.Click
        If SelectedGroup IsNot Nothing Then
            SaveChanges()
            'Tancar finalment
            Me.Close()
        Else
            MsgBox("Has de seleccionar un grup!")
        End If
       
    End Sub

    Private Sub ProfileEditor_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        LoadLocalValues()
    End Sub
End Class
