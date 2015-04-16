Imports TheLazyClientMVVM
Class MainWindowElements

    Private Sub btnClose_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles btnClose.MouseDown
        Me.Close()
        End
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
        Try
            DragMove()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim frm As New Login
        frm.ShowDialog()
        'Loginform > Invoke() > UpdateUI()
        'SetFilterHandlers()
        SetChatHandlers()
        AddKeyboadHandlers()
    End Sub

    Const NO_STATUS_TEXT As String = "Introdueix un estat..."
    Public Sub UpdateUI()
        lblUsername.Content = String.Format("{0} ({1})", c.localUser.real_name, c.localUser.username)
        lblCredits.Content = c.localUser.balance
        lstUsers.Items.Clear()
        Dim status As String = c.localStatus
        txtStatus.Text = If(status <> "", status, NO_STATUS_TEXT)

        For Each user As String In c.registrats
            lstUsers.Items.Add(user)
        Next
    End Sub
    <Obsolete("Utilitza UpdateHeavyElements() o UpdateUI().")> _
    Public Sub UpdateHeading() 'Legacy
        UpdateUI()
        'PROVA
        'c.filter.getFilteredElementsAsync()
        'FillPurchaseElementsTab()
        'FillElementTabComboboxes()
    End Sub
    Public Sub UpdateHeavyElements()
        'FillElementsTab()
        'FillPurchaseElementsTab()
        'c.filter.getFilteredElementsAsync()
        'FillElementTabComboboxes()
    End Sub

    'prova
    'Sub FillPurchaseElementsTab()
    'pnlPurchaseElements.Children.Clear()
    'For Each e As Entities.ElementPurchaseEntity In c.
    'End Sub

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
        Dim frm As New ProfileViewer
        frm.ShowDialog()
    End Sub
    Private Sub btnPerfilEdit_Click(sender As Object, e As RoutedEventArgs) Handles btnPerfilEdit.Click
        Dim frm As New ProfileEditor
        frm.ShowDialog()
        c.getHeadingInfo()
        UpdateUI()
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
    
    'Messaging
    Sub SetChatHandlers()
        AddHandler c.chatManager.rooms(0).MessageRecieved, AddressOf MessageRecieved
        AddHandler c.chatManager.rooms(0).Joined, AddressOf Joined
        AddHandler c.chatManager.rooms(0).UserListUpdated, AddressOf UserListUpdated
    End Sub
    Sub MessageRecieved()
        ' DO YOUR If... ELSE STATEMNT HERE
        txtChatLog.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, New Action(
        Sub()
            txtChatLog.Clear()
            For Each l As String In c.chatManager.rooms(0).messages
                txtChatLog.AppendText(l & vbCrLf)
            Next
        End Sub))

    End Sub
    Sub Joined()
        txtChatLog.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, New Action(
       Sub()
           txtChatLog.Clear()
           UserListUpdated()
       End Sub))
    End Sub
    Sub UserListUpdated()
        lstUsers.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, New Action(
      Sub()
          With lstUsers1.Items
              .Clear()
              For Each u As String In c.chatManager.rooms(0).getUserList()
                  .Add(u)
              Next
          End With
      End Sub))
    End Sub
    Private Sub btnSendMessage_Click(sender As Object, e As RoutedEventArgs) Handles btnSendMessage.Click
        SendChatMessage()
    End Sub
    Sub SendChatMessage()
        If txtSendMessage.Text = "" Then Exit Sub
        c.chatManager.rooms(0).sendMessage(txtSendMessage.Text)
        txtSendMessage.Clear()
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdate.Click
        UserListUpdated()
    End Sub

    Private Sub txtSendMessage_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSendMessage.KeyDown
        If e.Key = Key.Enter Then
            SendChatMessage()
        End If
    End Sub

    Private Sub txtSendMessage_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtSendMessage.TextChanged
        If IsLoaded Then

        End If

    End Sub
    

    'Stealth mode
    Sub StealthSwitch(value As Boolean)
        Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, New Action(
        Sub()
            For Each w As Window In Application.Current.Windows
                If value Then
                    w.Hide()
                Else
                    w.Show()
                End If
            Next

        End Sub))
    End Sub

    Dim stealth As Boolean = False
    Sub AddKeyboadHandlers()
        AddHandler hook.KeyDown, AddressOf OnKey
    End Sub
    Private Sub btnStealth_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles btnStealth.MouseDown
        Dim frm As New StealthModeUI
        frm.ShowDialog()
    End Sub
    Sub OnKey(sender As Object, e As RawKeyEventArgs)
        If e.Key = Key.Escape Then
            stealth = Not stealth
            StealthSwitch(stealth)
        End If
    End Sub

    Private Sub btnFTP_Click(sender As Object, e As RoutedEventArgs) Handles btnFTP.Click
        Dim w As New ElementExplorer
        w.Element = New Entities.ElementEntity With {.id = 4}
        w.Show()
    End Sub
End Class
