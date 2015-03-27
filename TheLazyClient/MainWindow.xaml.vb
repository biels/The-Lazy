Imports TheLazyClientMVVM
Class MainWindow

    Private Sub btnClose_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles btnClose.MouseDown
        Me.Close()
    End Sub

    Private Sub btnMinimize_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles btnMinimize.MouseDown
        Me.WindowState = Windows.WindowState.Minimized
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim frm As New ElementEditor
        frm.Element = New Entities.ElementEntity
        frm.IsNew = True
        frm.Show()
    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        DragMove()
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim frm As New Login
        frm.ShowDialog()
        '
        c.getHeadingInfo()
        UpdateHeading()
    End Sub
    Const NO_STATUS_TEXT As String = "Introdueix un estat..."
    Public Sub UpdateHeading()
        lblUsername.Content = String.Format("{0} ({1})", c.localUser.real_name, c.localUser.username)
        lblCredits.Content = c.localUser.balance
        lstUsers.Items.Clear()
        Dim status As String = c.localStatus
        txtStatus.Text = If(status <> "", status, NO_STATUS_TEXT)

        For Each user As String In c.registrats
            lstUsers.Items.Add(user)
        Next
        'PROVA
        pnlElements.Children.Clear()
        For i As Integer = 0 To 10
            pnlElements.Children.Add(New ElementThumbnaiItem())
        Next
        FillElementTabComboboxes()
    End Sub


    Private Sub lstUsers_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles lstUsers.MouseDoubleClick
        Dim f As New UserProfileViewer
        f.User = c.getUserInfo(lstUsers.SelectedItem)
        f.Show()
    End Sub
    Function ShouldSubmitStatus() As Boolean
        Try
            Return Not (txtStatus.Text = NO_STATUS_TEXT Or txtStatus.Text = "" Or txtStatus.Text = c.localUser.status)

        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub txtStatus_KeyDown(sender As Object, e As KeyEventArgs) Handles txtStatus.KeyDown
        If e.Key = Key.Enter And ShouldSubmitStatus() Then
            c.localStatus = txtStatus.Text
            c.updateLocalUser()
            UpdateStatusLabel()
        End If
    End Sub
    Sub UpdateStatusLabel()
        lblStatus.Content = If(ShouldSubmitStatus(), "[ENTER] per establir el nou estat", "Estat")
    End Sub
    Private Sub txtStatus_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtStatus.TextChanged
        UpdateStatusLabel()
    End Sub

    Private Sub btnPerfil_Click(sender As Object, e As RoutedEventArgs) Handles btnPerfil.Click
        Dim frm As New ProfileEditor
        frm.ShowDialog()
        c.getHeadingInfo()
        UpdateHeading()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnExit.Click
        End
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim frm As New AdministrationMenu
        frm.Show()
    End Sub

    Private Sub ListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    '------------------
    Sub FillElementTabComboboxes()
        If c.filter._AcademicLevels Is Nothing Then Exit Sub
        With cmbAcademicLevels.Items
            .Clear()
            For Each e As Entities.AcademicLevelEntity In c.filter._AcademicLevels
                .Add(e)
            Next
        End With

    End Sub
    Sub UpdateSubjectsCombobox()
        If c.filter._Subjects Is Nothing Then Exit Sub
        With cmbSubjectFilter.Items
            .Clear()
            For Each e As Entities.SubjectEntity In c.filter._Subjects
                .Add(e)
            Next
        End With
    End Sub
    '------------------

    Private Sub cmbAcademicLevels_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbAcademicLevels.SelectionChanged
        c.filter.updateSubjectList(cmbAcademicLevels.SelectedItem)
        UpdateSubjectsCombobox()
    End Sub
End Class
