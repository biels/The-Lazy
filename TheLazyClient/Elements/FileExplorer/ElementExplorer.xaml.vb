Imports TheLazyClientMVVM

Public Class ElementExplorer

    Private Sub ElementExplorer_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        FillRemoteFileSystem()
    End Sub
    Private _Element As Entities.ElementEntity
    Public Property Element As Entities.ElementEntity
        Get
            Return _Element
        End Get
        Set(ByVal value As Entities.ElementEntity)
            _Element = value
        End Set
    End Property
    Sub FillRemoteFileSystem()

        Dim control As New RemoteFilesystemViewer
        control.Element = Element
        pnlRemoteFileSystem.Children.Add(control)
    End Sub

End Class



