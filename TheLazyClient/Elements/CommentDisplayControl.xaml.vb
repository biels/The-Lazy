Imports TheLazyClientMVVM
Public Class CommentDisplayControl
    Public Event EditCommentClick(Comment As Entities.ElementCommentEntity)
    Public Event DeleteCommentClick(Comment As Entities.ElementCommentEntity)
    Private _Comment As Entities.ElementCommentEntity
    Public Property Comment() As Entities.ElementCommentEntity
        Get
            Return _Comment
        End Get
        Set(ByVal value As Entities.ElementCommentEntity)
            _Comment = value
            UpdateUI()
        End Set
    End Property

    Sub UpdateUI()
        If Comment IsNot Nothing Then
            With Comment
                lblUsername.Content = .user.real_name
                lblUsername.ToolTip = .user.username
                lblTimestamp.Content = String.Format("{0} ({1}){2}", .create_time, TimeAgo(.create_time), If(.hasBeenEdited, " - Editat", "")) '11/09/16 22:58 (fa 3 dies)
                lblTimestamp.ToolTip = If(.hasBeenEdited, String.Format("Modificat per última vegada el {0} ({1})", .update_time, TimeAgo(.update_time)), "No s'ha modificat")
                txtText.Text = .text
                txtText.ToolTip = .text
                txtText.LineStackingStrategy = LineStackingStrategy.MaxHeight
                'txtText.si
                imgEditComment.Visibility = If(.isFromLocalUser(), Windows.Visibility.Visible, Windows.Visibility.Collapsed)
                imgDeleteComment.Visibility = imgEditComment.Visibility
            End With
        End If
    End Sub
    Private Sub imgEditComment_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles imgEditComment.MouseDown
        e.Handled = True
        RaiseEvent EditCommentClick(Comment)
    End Sub
    Private Sub imgDeleteComment_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles imgDeleteComment.MouseDown
        e.Handled = True
        RaiseEvent DeleteCommentClick(Comment)
    End Sub

    Private Sub txtText_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles txtText.SizeChanged

    End Sub
End Class
