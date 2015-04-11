Public Class ProfileViewerControl
    Private _Username As String
    Public Property Username() As String
        Get
            Return _Username
        End Get
        Set(ByVal value As String)
            _Username = value
        End Set
    End Property

    Private Sub txtProfileName_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtProfileName.GotFocus
        txtProfileName.Clear()
    End Sub
    Private Sub txtProfileName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProfileName.KeyDown
        If e.Key = Key.Enter Then
            MsgBox("Funció no implementada")
        End If
    End Sub
End Class
