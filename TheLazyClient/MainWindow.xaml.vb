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
        '
        c.getHeadingInfo()
        UpdateHeading()
    End Sub

    Public Sub UpdateHeading()
        lblUsername.Content = c.loginManager.username
        lstUsers.Items.Clear()
        Dim status As String = c.localStatus
        txtStatus.Text = If(status <> "", status, "Introdueix un estat...")

        For Each user As String In c.registrats
            lstUsers.Items.Add(user)
        Next
    End Sub

    Private Sub ListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    Private Sub lstUsers_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles lstUsers.MouseDoubleClick
        Dim f As New UserProfileViewer
        f.User = c.getUserInfo(lstUsers.SelectedItem)
        f.Show()
    End Sub

    Private Sub txtStatus_KeyDown(sender As Object, e As KeyEventArgs) Handles txtStatus.KeyDown
        If e.Key = Key.Enter Then
            c.localStatus = txtStatus.Text
        End If
    End Sub

    Private Sub txtStatus_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtStatus.TextChanged

    End Sub
End Class
